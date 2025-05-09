using AuthService.Domain.Constants;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Seeders;

internal class Seeder : ISeeder
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly UserManager<User> _userManager;

    public Seeder(RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        await SeedUserRoles();
        await SeedUsers();
    }

    private async Task SeedUserRoles()
    {
        if (_roleManager.Roles.Any()) return;

        foreach (var roleName in DummyData.AllUserRoles)
        {
            var role = new IdentityRole<int>(roleName);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Seeding role '{roleName}' " +
                    $"failed: {string.Join(',', result.Errors.Select(e => e.Description))}");
            }
        }
    }

    private async Task SeedUsers()
    {
        if (_userManager.Users.Any()) return;

        foreach (var (username, password, role) in DummyData.Users)
        {
            var newUser = new User
            {
                UserName = username,
                Email = username
            };

            var createResult = await _userManager.CreateAsync(newUser, password);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Creating user '{username}' " +
                    $"failed: {string.Join(',', createResult.Errors.Select(e => e.Description))}");
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, role);
            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Assigning role '{role}' to user '{username}' " +
                    $"failed: {string.Join(',', roleResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}
