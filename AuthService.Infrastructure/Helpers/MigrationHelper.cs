using AuthService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.Helpers;

public static class MigrationHelper
{
    public static void ApplyAuthMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        dbContext.Database.Migrate();
    }
}
