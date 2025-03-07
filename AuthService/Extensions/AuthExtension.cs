using System.Text;
using AuthService.Authentication;
using AuthService.DAL.Enums;
using AuthService.Utility.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Extensions;

public static class AuthExtension
{
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer",options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddAuthorization(options =>
        {
            Permission[] adminPermissions = [AuthService.DAL.Enums.Permission.Create];
            options.AddPolicy("Admin", policy => policy.AddRequirements(new PermissionRequirement(adminPermissions)));
        });
    }
    public static IEndpointConventionBuilder RequirePermissions<TBuilder>
        (this TBuilder builder, params Permission[] permissions) where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(permissions)));
    }
}