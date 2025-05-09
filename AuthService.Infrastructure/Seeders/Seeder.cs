using AuthService.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Seeders;

internal class Seeder : ISeeder
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public Seeder(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await SeedUserRoles();
    }

    private async Task SeedUserRoles()
    {
        var roles = UserRoles.All;

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole<int>(roleName);
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Seeding role '{roleName}' failed: {string.Join(',', result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
