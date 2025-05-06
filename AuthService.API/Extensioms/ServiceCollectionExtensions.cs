using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace AuthService.API.Extensioms;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddIdentityApiEndpoints<User>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                    },
                    []
                }
            });
        });
    }
}