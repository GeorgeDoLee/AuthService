using AuthService.Application.Services;
using AuthService.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AuthService.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
          .AddOptions<JwtSettings>()
          .Bind(configuration.GetSection("Jwt"))
          .ValidateDataAnnotations()
          .ValidateOnStart();

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<JwtSettings>>().Value);

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
    }
}
