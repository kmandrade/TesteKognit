using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int userId);
    Task<List<User>?> GetAllUserAsync();
}

