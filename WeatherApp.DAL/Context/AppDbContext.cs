using Microsoft.EntityFrameworkCore;

namespace WeatherApp.DAL.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}