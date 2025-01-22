using Domain.Entities;
using FluentResults;
using ViewModel.User;

namespace Domain.Interfaces.Service;

public interface IUserService
{
    Task<Result<UserViewModel>> CreateUserAsync(UserViewModel model);
    Task<List<UserViewModel>?> GetAllUsersAsync();
}

