using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserById(int userId);
}

