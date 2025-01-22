using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using FluentResults;
using Microsoft.Extensions.Logging;
using ViewModel.User;
using ViewModel.Wallet;

namespace Service.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<WalletService> _logger;

    public WalletService(IWalletRepository walletRepository,
                         IUserRepository userRepository,
                         ILogger<WalletService> logger)
    {
        _walletRepository = walletRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Result<WalletViewModel>> CreateWalletAsync(WalletViewModel model)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(model.UserId);

            if (user == null)
                return Result.Fail("Usuário não encontrado.");

            var result = await _walletRepository.CreateWallerAsync(new Wallet
            {
                User = user,
                UserId = user.Id,
                Bank = model.Bank,
                DateCreated = DateTime.Now,
                Active = true,
                CurrentValue = model.CurrentValue
            });

            var wallerViewModel = new WalletViewModel
            {
                Id = result.Id,
                UserId = user.Id,
                Bank = result.Bank,
                Active = true,
                CurrentValue = result.CurrentValue
            };

            _logger.LogInformation("Carteira criada com sucesso.");
            return Result.Ok(wallerViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao criar uma nova carteira.");
            return Result.Fail("Não foi possível criar uma nova carteira, por favor tente novamente.");
        }
    }

    public async Task<List<WalletsUserViewModel>?> GetWalletsByCpfUserAsync(string nrCpf)
    {
        var result = await _walletRepository.GetWalletsByCpfUserAsync(nrCpf);
        
        if (result == null || !result.Any())
            return new List<WalletsUserViewModel>();

        return result.Select(x => new WalletsUserViewModel
        {
            Active = x.Active,
            Bank = x.Bank,
            CurrentValue = x.CurrentValue,
            DateModified = x.DateModified,
            User = new UserViewModel
            {
                Name = x.User.Name,
                NrCpf = x.User.NrCpf,
                Active = x.Active,
                UserId = x.User.Id,
            }
        }).ToList();
    }

    public async Task<List<WalletsUserViewModel>?> GetAllWalletsAsync()
    {
        var result = await _walletRepository.GetAllWalletsAsync();

        if (result == null)
            return new List<WalletsUserViewModel>();

        return result.Select(x => new WalletsUserViewModel
        {
            Active = x.Active,
            Bank = x.Bank,
            CurrentValue = x.CurrentValue,
            DateModified = x.DateModified,
            User = new UserViewModel
            {
                Name = x.User.Name,
                NrCpf = x.User.NrCpf,
                Active = x.Active,
                UserId = x.User.Id,
            }
        }).ToList();
    }
}
