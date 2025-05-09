using AuthService.Application.Models;
using AuthService.Application.Services;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = AuthService.Application.Services.IAuthorizationService;

namespace AuthService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthorizationService authorizationService, ITokenService tokenService)
    {
        _authorizationService = authorizationService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]LoginUser loginUser)
    {
        var userRegistered = await _authorizationService.RegisterUserAsync(loginUser);

        return userRegistered ? 
            Ok("registered successfully") 
            : 
            BadRequest("something went wrong");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginUser loginUser)
    {
        var loggedInUser = await _authorizationService.LoginUserAsync(loginUser);

        if (loggedInUser != null)
        {
            var token = await _tokenService.GenerateTokenAsync(loggedInUser);
            return Ok(token);
        }

        return BadRequest("something went wrong");
    }
}
