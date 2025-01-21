using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using FluentResults;
using Microsoft.Extensions.Logging;
using ViewModel.User;

namespace Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Result<UserViewModel>> CreateUserAsync(UserViewModel model)
    {
        try
        {
            var result = await _userRepository.CreateUserAsync(new User
            {
                Active = true,
                BithDate = model.BithDate,
                DateCreated = DateTime.Now,
                Name = model.Name,
                NrCpf = model.NrCpf
            });
            var viewModel = new UserViewModel
            {
                NrCpf = result.NrCpf,
                Name = result.Name,
                BithDate = result.BithDate,
                UserId = result.Id
            };

            return Result.Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao criar um novo usuário.");
            return Result.Fail("Não foi possível criar um novo usuário, por favor tente novamente");
        }
    }
}
