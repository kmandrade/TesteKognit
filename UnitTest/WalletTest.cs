using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Services;
using ViewModel.Wallet;

namespace UnitTest;

public class WalletTest
{
    private readonly Mock<IWalletRepository> _mockWalletRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ILogger<WalletService>> _mockLogger;
    private readonly WalletService _walletService;

    public WalletTest()
    {
        _mockWalletRepository = new Mock<IWalletRepository>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<WalletService>>();
        _walletService = new WalletService(
            _mockWalletRepository.Object,
            _mockUserRepository.Object,
            _mockLogger.Object
        );
    }
    #region CreateWallet
    [Fact]
    public async Task CreateWalletAsync_ValidModel_ReturnsWalletViewModel()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "João Silva",
            NrCpf = "123.456.789-00",
            Active = true
        };

        var wallet = new Wallet
        {
            Id = 1,
            User = user,
            UserId = user.Id,
            Bank = "Banco do Brasil",
            DateCreated = DateTime.Now,
            Active = true,
            CurrentValue = 100.0m
        };

        _mockUserRepository
            .Setup(repo => repo.GetUserByIdAsync(user.Id))
            .ReturnsAsync(user);

        _mockWalletRepository
            .Setup(repo => repo.CreateWallerAsync(It.IsAny<Wallet>()))
            .ReturnsAsync(wallet);

        var model = new WalletViewModel
        {
            UserId = user.Id,
            Bank = "Banco do Brasil",
            CurrentValue = 100.0m
        };

        // Act
        var result = await _walletService.CreateWalletAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Banco do Brasil", result.Value.Bank);
        Assert.Equal(100.0m, result.Value.CurrentValue);
        Assert.Equal(user.Id, result.Value.UserId);
    }

    [Fact]
    public async Task CreateWalletAsync_UserNotFound_ReturnsFailResult()
    {
        // Arrange
        _mockUserRepository
            .Setup(repo => repo.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((User)null);

        var model = new WalletViewModel
        {
            UserId = 1,
            Bank = "Banco do Brasil",
            CurrentValue = 100.0m
        };

        // Act
        var result = await _walletService.CreateWalletAsync(model);

        // Assert
        Assert.False(result.IsSuccess);
        var error = result.Errors.ToList()[0];
        Assert.Contains("Usuário não encontrado.", error.Message);
    }

    #endregion

    [Fact]
    public async Task GetWalletsByCpfUserAsync_ValidCpf_ReturnsWallets()
    {
        // Arrange
        var cpf = "123.456.789-00";
        var wallets = new List<Wallet>
        {
            new Wallet
            {
                Active = true,
                Bank = "Banco do Brasil",
                CurrentValue = 1500.75m,
                DateModified = DateTime.Now,
                User = new User
                {
                    Id = 1,
                    Name = "João Silva",
                    NrCpf = cpf,
                    Active = true
                }
            }
        };

        _mockWalletRepository
            .Setup(repo => repo.GetWalletsByCpfUserAsync(cpf))
            .ReturnsAsync(wallets);

        // Act
        var result = await _walletService.GetWalletsByCpfUserAsync(cpf);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Banco do Brasil", result[0].Bank);
        Assert.Equal(cpf, result[0].User.NrCpf);
    }
}
