using AuthService.DAL.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(Permission[] permissions)
    {
        Permissions = permissions;
    }
    public Permission[] Permissions { get; set; } = [];
}