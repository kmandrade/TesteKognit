
using Domain.Context;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly MyContext _context;
    public WalletRepository(MyContext context)
    {
        _context = context;
    }

    public async Task<Wallet> CreateWallerAsync(Wallet wallet)
    {
        _context.Set<Wallet>().Add(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task<List<Wallet>?> GetAllWalletsAsync()
    {
        return await _context.Set<Wallet>()
            .Include(x => x.User)
            .ToListAsync();
    }

    public async Task<List<Wallet>?> GetWalletsByCpfUserAsync(string cpfUser)
    {
        return await _context.Set<Wallet>()
             .Include(x => x.User)
             .Where(x => x.User.NrCpf.Equals(cpfUser))
             .ToListAsync();
    }
}

