using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ApolloAesthetics.Application.DTOs.Auth;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Domain.Entities;
using ApolloAesthetics.Domain.Constants;

namespace ApolloAesthetics.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDto model)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateEmail",
                    Description = "Email is already registered"
                });
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth ?? DateTime.MinValue,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                // Assign default role (Patient)
                await _userManager.AddToRoleAsync(user, Roles.Patient);
                
                _logger.LogInformation($"User {model.Email} registered successfully");
            }
            else
            {
                _logger.LogWarning($"Failed to register user {model.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while registering user {model.Email}");
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RegistrationError",
                Description = "An error occurred during registration"
            });
        }
    }

    public async Task<SignInResult> LoginAsync(LoginDto model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !user.IsActive)
            {
                _logger.LogWarning($"Login attempt for inactive or non-existent user: {model.Email}");
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {model.Email} logged in successfully");
            }
            else
            {
                _logger.LogWarning($"Failed login attempt for user {model.Email}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while logging in user {model.Email}");
            return SignInResult.Failed;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while logging out user");
        }
    }

    public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto model)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found"
                });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            
            if (result.Succeeded)
            {
                _logger.LogInformation($"Password changed successfully for user {user.Email}");
            }
            else
            {
                _logger.LogWarning($"Failed to change password for user {user.Email}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while changing password for user {userId}");
            return IdentityResult.Failed(new IdentityError
            {
                Code = "PasswordChangeError",
                Description = "An error occurred while changing password"
            });
        }
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return string.Empty;
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while generating password reset token for {email}");
            return string.Empty;
        }
    }

    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found"
                });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation($"Password reset successfully for user {model.Email}");
            }
            else
            {
                _logger.LogWarning($"Failed to reset password for user {model.Email}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while resetting password for {model.Email}");
            return IdentityResult.Failed(new IdentityError
            {
                Code = "PasswordResetError",
                Description = "An error occurred while resetting password"
            });
        }
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return string.Empty;
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while generating email confirmation token for user {userId}");
            return string.Empty;
        }
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found"
                });
            }

            return await _userManager.ConfirmEmailAsync(user, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while confirming email for user {userId}");
            return IdentityResult.Failed(new IdentityError
            {
                Code = "EmailConfirmationError",
                Description = "An error occurred while confirming email"
            });
        }
    }

    public async Task<bool> IsEmailConfirmedAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.EmailConfirmed ?? false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while checking email confirmation for user {userId}");
            return false;
        }
    }

    public async Task<UserDto?> GetUserByIdAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                DateOfBirth = user.DateOfBirth,
                ProfilePicture = user.ProfilePicture ?? string.Empty,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting user {userId}");
            return null;
        }
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                DateOfBirth = user.DateOfBirth,
                ProfilePicture = user.ProfilePicture ?? string.Empty,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting user by email {email}");
            return null;
        }
    }

    public async Task<bool> IsUserInRoleAsync(string userId, string role)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            return await _userManager.IsInRoleAsync(user, role);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while checking role {role} for user {userId}");
            return false;
        }
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting roles for user {userId}");
            return new List<string>();
        }
    }

    public Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        throw new NotImplementedException( );
    }
}

