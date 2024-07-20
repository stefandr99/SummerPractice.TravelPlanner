namespace TravelPlanner.Application.Services.Interfaces;

using Models.Common;
using Models.User.DTO;

public interface IUserService
{
    Task<Result<UserIdentifictionDTO>> AuthenticateAsync(string username, string password);

    Task<Result<UserIdentifictionDTO>> RegisterAsync(string username, string password, string email);

    Task<Result<UserDTO>> GetUserByIdAsync(int id);

    Task<Result<UserDTO>> GetUserByUsernameOrEmailAsync(string usernameOrEmail);

    Task<Result> VerifyEmailAsync(int userId, string verificationToken);
}