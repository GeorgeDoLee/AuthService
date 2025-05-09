using AuthService.Application.Models.Requests;
using AuthService.Domain.Entities;

namespace AuthService.Application.Services;

public interface IAuthorizationService
{
    Task<bool> RegisterUserAsync(LoginUser loginUser);

    Task<User?> LoginUserAsync(LoginUser loginUser);
}