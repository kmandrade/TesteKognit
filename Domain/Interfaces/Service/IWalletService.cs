using FluentResults;
using ViewModel.Wallet;

namespace Domain.Interfaces.Service;

public interface IWalletService
{
    Task<Result<WalletViewModel>> CreateWalletAsync(WalletViewModel model);
    Task<List<WalletsUserViewModel>?> GetWalletsByCpfUserAsync(string nrCpf);
}
