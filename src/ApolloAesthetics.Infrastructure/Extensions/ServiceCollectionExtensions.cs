using ApolloAesthetics.Domain.Constants;
using ApolloAesthetics.Domain.Entities;
using ApolloAesthetics.Domain.Interfaces;
using ApolloAesthetics.Infrastructure.Data;
using ApolloAesthetics.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApolloAesthetics.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Add Identity
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Configure Application Cookie
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        // Add Authorization
        services.AddAuthorization(options =>
        {
            // Example: Use policies defined in ApolloAesthetics.Domain.Constants.Policies
            options.AddPolicy(Policies.AdminOnly,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin));
            options.AddPolicy(Policies.AdminOrSuperAdmin,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin));
            options.AddPolicy(Policies.DoctorOnly,policy => policy.RequireRole(Roles.Doctor));
            options.AddPolicy(Policies.DoctorOrAdmin,policy => policy.RequireRole(Roles.Doctor,Roles.Admin));
            options.AddPolicy(Policies.StaffOrAbove,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin,Roles.Staff));
            options.AddPolicy(Policies.MedicalStaff,policy => policy.RequireRole(Roles.Doctor,Roles.Staff));
            options.AddPolicy(Policies.PatientOnly,policy => policy.RequireRole(Roles.Patient));
            options.AddPolicy(Policies.PatientOrMedicalStaff,policy => policy.RequireRole(Roles.Patient,Roles.Doctor,Roles.Staff));
            options.AddPolicy(Policies.AuthenticatedUser,policy => policy.RequireAuthenticatedUser( ));
            options.AddPolicy(Policies.ManageAppointments,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin,Roles.Doctor,Roles.Staff));
            options.AddPolicy(Policies.ManagePatients,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin,Roles.Doctor,Roles.Staff));
            options.AddPolicy(Policies.ManageConsultations,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin,Roles.Doctor));
            options.AddPolicy(Policies.ViewReports,policy => policy.RequireRole(Roles.SuperAdmin,Roles.Admin,Roles.Doctor));
            options.AddPolicy(Policies.ManageSystem,policy => policy.RequireRole(Roles.SuperAdmin));
        });

        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

