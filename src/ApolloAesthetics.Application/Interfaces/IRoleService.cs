using ApolloAesthetics.Application.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace ApolloAesthetics.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto?> GetRoleByIdAsync(string roleId);
    Task<RoleDto?> GetRoleByNameAsync(string roleName);
    Task<IdentityResult> CreateRoleAsync(CreateRoleDto model);
    Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto model);
    Task<IdentityResult> DeleteRoleAsync(string roleId);
    Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDto model);
    Task<IdentityResult> RemoveRoleFromUserAsync(RemoveRoleDto model);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName);
    Task<bool> RoleExistsAsync(string roleName);
    Task<int> GetRolesCountAsync();
}

