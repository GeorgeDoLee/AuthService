using AuthService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
    }
}
