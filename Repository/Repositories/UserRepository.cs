using Domain.Context;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyContext _context;
    public UserRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Set<User>().Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>?> GetAllUserAsync()
    {
        return await _context.Set<User>().ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User?> GetUserByNrCpf(string nrCpf)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(x => x.NrCpf == nrCpf);
    }
}

