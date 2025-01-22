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
            var user = await _userRepository.GetUserByNrCpf(model.NrCpf);
            
            if (user != null)
                return Result.Fail("Usuário já existe.");

            var result = await _userRepository.CreateUserAsync(new User
            {
                Active = true,
                BirthDate = Convert.ToDateTime(model.BirthDate),
                DateCreated = DateTime.Now,
                Name = model.Name,
                NrCpf = model.NrCpf
            });
            var viewModel = new UserViewModel
            {
                NrCpf = result.NrCpf,
                Name = result.Name,
                BirthDate = result.BirthDate.ToString(),
                UserId = result.Id,
                Active = result.Active
            };

            return Result.Ok(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao criar um novo usuário.");
            return Result.Fail("Não foi possível criar um novo usuário, por favor tente novamente");
        }
    }

    public async Task<List<UserViewModel>?> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUserAsync();

        if (users == null || !users.Any())
            return new List<UserViewModel>();

        return users.Select(x => new UserViewModel
        {
            BirthDate = x.BirthDate.ToString(),
            Name = x.Name,
            NrCpf = x.NrCpf,
            UserId = x.Id,
            Active = x.Active
        }).ToList();

    }
}
