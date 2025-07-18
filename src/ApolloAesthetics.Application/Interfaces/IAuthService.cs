using ApolloAesthetics.Application.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace ApolloAesthetics.Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto model);
    Task<SignInResult> LoginAsync(LoginDto model);
    Task LogoutAsync();
    Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto model);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto model);
    Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    Task<bool> IsEmailConfirmedAsync(string userId);
    Task<UserDto?> GetUserByIdAsync(string userId);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<bool> IsUserInRoleAsync(string userId, string role);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
}

