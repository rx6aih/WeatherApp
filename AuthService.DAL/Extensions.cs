using AuthService.DAL.Context;
using AuthService.DAL.Implementations;
using AuthService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DAL;

public static class Extensions
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddDbContext<AuthContext>(x =>
            x.UseNpgsql("Server=postgres;Port=5432;Database=Auth;User Id=postgres;Password=postgres"
            ));
        return services;
    }
}