using AuthService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AuthService.DAL.EntityTypeConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<RolePermission>(
                l => l.HasOne<Permission>().WithMany().HasForeignKey(r => r.PermissionId),
                r => r.HasOne<Role>().WithMany().HasForeignKey(l => l.RoleId));

        var roles = Enum
            .GetValues<Enums.Role>()
            .Select(r => new Role
            {
                Id = (int)r,
                Name = r.ToString(),
            });
        
        builder.HasData(roles);
    }
}