using ApolloAesthetics.Application.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace ApolloAesthetics.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string userId);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<IdentityResult> CreateUserAsync(CreateUserDto model);
    Task<IdentityResult> UpdateUserAsync(UpdateUserDto model);
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IdentityResult> ActivateUserAsync(string userId);
    Task<IdentityResult> DeactivateUserAsync(string userId);
    Task<UserProfileDto?> GetUserProfileAsync(string userId);
    Task<IdentityResult> UpdateUserProfileAsync(string userId, UpdateProfileDto model);
    Task<IEnumerable<UserDto>> GetUsersByRoleAsync(string roleName);
    Task<int> GetUsersCountAsync();
    Task<int> GetActiveUsersCountAsync();
}

