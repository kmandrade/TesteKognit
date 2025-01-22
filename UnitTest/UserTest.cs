using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using Service.Services;
using ViewModel.User;

namespace UnitTest;

public class UserTest
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<UserService>> _mockLogger;
    private readonly UserService _userService;

    public UserTest()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UserService>>();

        _userService = new UserService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task CreateUserAsync_ValidModel_ReturnsUserViewModel()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger<UserService>>();

        var user = new User
        {
            Id = 1,
            Name = "João Silva",
            BirthDate = new DateTime(1990, 1, 1),
            NrCpf = "123.456.789-00",
            Active = true,
            DateCreated = DateTime.Now
        };

        mockRepository
            .Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync(user);

        var service = new UserService(mockRepository.Object, mockLogger.Object);

        var model = new UserViewModel
        {
            Name = user.Name,
            BirthDate = user.BirthDate.ToString(),
            NrCpf = user.NrCpf
        };

        // Act
        var result = await service.CreateUserAsync(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("João Silva", result.Value.Name);
        Assert.Equal("123.456.789-00", result.Value.NrCpf);
    }

    [Fact]
    public async Task CreateUserAsync_ExceptionThrown_ReturnsFailResult()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger<UserService>>();

        mockRepository
            .Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
            .ThrowsAsync(new Exception("Database error"));

        var service = new UserService(mockRepository.Object, mockLogger.Object);

        var model = new UserViewModel
        {
            Name = "João Silva",
            BirthDate = "1990-01-01",
            NrCpf = "123.456.789-"
        };

        // Act
        var result = await service.CreateUserAsync(model);

        // Assert
        Assert.False(result.IsSuccess);
    }

}
