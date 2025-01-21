using AuthService.Services;
using AuthService.Utility;
using AuthService.Utility.Jwt;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<JwtProvider>();
        services.AddScoped<PasswordHasher>();
        return services;
    }
}