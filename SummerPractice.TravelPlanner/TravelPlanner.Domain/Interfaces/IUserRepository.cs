namespace TravelPlanner.Domain.Interfaces;

using Entities;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByUsernameAsync(string username);

    Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);

    Task AddUserAsync(User user);

    Task SaveAsync();
}