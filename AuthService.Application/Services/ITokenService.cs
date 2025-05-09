using AuthService.Application.Models;
using AuthService.Domain.Entities;

namespace AuthService.Application.Services;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
}
