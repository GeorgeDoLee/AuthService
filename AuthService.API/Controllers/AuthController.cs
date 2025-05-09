using AuthService.API.Responses;
using AuthService.Application.Models.Requests;
using AuthService.Application.Models.Responses;
using AuthService.Application.Services;
using AuthService.Application.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IAuthorizationService = AuthService.Application.Services.IAuthorizationService;

namespace AuthService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwt;

    public AuthController(
        IAuthorizationService authorizationService, 
        ITokenService tokenService,
        JwtSettings jwt)
    {
        _authorizationService = authorizationService;
        _tokenService = tokenService;
        _jwt = jwt;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]LoginUser loginUser)
    {
        var userRegistered = await _authorizationService.RegisterUserAsync(loginUser);

        return userRegistered ? 
            Ok(ApiResponse<string>.SuccessResponse("Registration finished successfully.")) 
            : 
            BadRequest(ApiResponse<string>.FailResponse("Registration failed."));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
    {
        var loggedInUser = await _authorizationService.LoginUserAsync(loginUser);

        if (loggedInUser != null)
        {
            var token = await _tokenService.GenerateTokenAsync(loggedInUser);

            var jwtSettings = HttpContext.RequestServices
                .GetRequiredService<IOptions<JwtSettings>>().Value;

            var response = new LoginResponse
            {
                TokenType = "Bearer",
                Token = token,
                ExpiresInMinutes = jwtSettings.ExpirationMinutes
            };

            return Ok(ApiResponse<LoginResponse>.SuccessResponse(response, "Login successful"));
        }

        return BadRequest(ApiResponse<string>.FailResponse("Invalid credentials"));
    }
}
