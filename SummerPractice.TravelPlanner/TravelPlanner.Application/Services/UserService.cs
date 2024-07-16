namespace TravelPlanner.Application.Services;

using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using Interfaces;
using Mappers.User;
using Models.Common;
using Models.User.DTO;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Result> AuthenticateAsync(string username, string password)
    {
        var user = await userRepository.GetUserByUsernameAsync(username);

        if (user == null || user.Password != this.EncryptPassword(password))
        {
            return new ErrorResult("Authentication failed");
        }

        return new SuccessResult();
    }

    public async Task<Result> RegisterAsync(string username, string password, string email)
    {
        var existingUser = await userRepository.GetUserByUsernameAsync(username);

        if (existingUser != null)
        {
            throw new ArgumentException("Username already exists");
        }

        var encryptedPassword = this.EncryptPassword(password);

        var user = new User
        {
            Username = username,
            Password = encryptedPassword,
            Email = email,
            IsEmailVerified = false
        };

        await userRepository.AddUserAsync(user);
        await userRepository.SaveAsync();

        return new SuccessResult();
    }

    public async Task<Result> VerifyEmailAsync(int userId, string verificationToken)
    {

        var user = await userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        if (verificationToken == "valid_token")
        {
            user.IsEmailVerified = true;

            await userRepository.SaveAsync();

            return new SuccessResult();
        }

        throw new ArgumentException("Invalid verification token");
    }

    public async Task<Result<UserDTO>> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        var userDto = UserMapping.ToUserDTO(user);

        return new SuccessResult<UserDTO>(userDto);
    }

    public async Task<Result<UserDTO>> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        var user = await userRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail);

        var userDto = UserMapping.ToUserDTO(user);

        return new SuccessResult<UserDTO>(userDto);
    }

    private string EncryptPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }
}