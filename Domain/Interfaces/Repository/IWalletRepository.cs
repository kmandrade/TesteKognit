using Domain.Entities;

namespace Domain.Interfaces.Repository;

public interface IWalletRepository
{
    Task<Wallet> CreateWallerAsync(Wallet wallet);
    Task<List<Wallet>?> GetWalletsByCpfUserAsync(string cpfUser);
}
