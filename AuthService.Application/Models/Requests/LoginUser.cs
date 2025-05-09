namespace AuthService.Application.Models.Requests;

public class LoginUser
{
    public required string Username { get; set; }

    public required string Password { get; set; }
}