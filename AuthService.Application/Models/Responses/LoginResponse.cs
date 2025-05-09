namespace AuthService.Application.Models.Responses;

public class LoginResponse
{
    public required string TokenType { get; set; }
    public required string Token { get; set; }
    public required int ExpiresInMinutes { get; set; }
}
