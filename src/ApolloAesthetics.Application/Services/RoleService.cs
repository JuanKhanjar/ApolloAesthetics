using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApolloAesthetics.Application.DTOs.Auth;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Application.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var roleDtos = new List<RoleDto>();

        foreach (var role in roles)
        {
            var userCount = await _userManager.GetUsersInRoleAsync(role.Name!);
            roleDtos.Add(new RoleDto
            {
                Id = role.Id,
                Name = role.Name!,
                Description = role.Description ?? string.Empty,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt,
                UserCount = userCount.Count
            });
        }

        return roleDtos;
    }

    public async Task<RoleDto?> GetRoleByIdAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null) return null;

        var userCount = await _userManager.GetUsersInRoleAsync(role.Name!);
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name!,
            Description = role.Description ?? string.Empty,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt,
            UserCount = userCount.Count
        };
    }

    public async Task<RoleDto?> GetRoleByNameAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null) return null;

        var userCount = await _userManager.GetUsersInRoleAsync(role.Name!);
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name!,
            Description = role.Description ?? string.Empty,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt,
            UserCount = userCount.Count
        };
    }

    public async Task<IdentityResult> CreateRoleAsync(CreateRoleDto model)
    {
        var role = new ApplicationRole(model.Name, model.Description)
        {
            IsActive = model.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> UpdateRoleAsync(UpdateRoleDto model)
    {
        var role = await _roleManager.FindByIdAsync(model.Id);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
        }

        role.Name = model.Name;
        role.NormalizedName = model.Name.ToUpper();
        role.Description = model.Description;
        role.IsActive = model.IsActive;
        role.UpdatedAt = DateTime.UtcNow;

        return await _roleManager.UpdateAsync(role);
    }

    public async Task<IdentityResult> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
        }

        return await _roleManager.DeleteAsync(role);
    }

    public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        if (!await _roleManager.RoleExistsAsync(model.RoleName))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
        }

        return await _userManager.AddToRoleAsync(user, model.RoleName);
    }

    public async Task<IdentityResult> RemoveRoleFromUserAsync(RemoveRoleDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        return await _userManager.RemoveFromRoleAsync(user, model.RoleName);
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return new List<string>();

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName)
    {
        var users = await _userManager.GetUsersInRoleAsync(roleName);
        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber?? string.Empty,
                DateOfBirth = user.DateOfBirth,
                ProfilePicture = user.ProfilePicture ?? string.Empty,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles
            });
        }

        return userDtos;
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    public async Task<int> GetRolesCountAsync()
    {
        return await _roleManager.Roles.CountAsync();
    }
}

