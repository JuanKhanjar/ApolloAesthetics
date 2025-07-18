using ApolloAesthetics.Domain.Constants;
using ApolloAesthetics.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApolloAesthetics.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger? logger = null)
    {
        try
        {
            // Seed Roles
            await SeedRolesAsync(roleManager, logger);

            // Seed Users
            await SeedUsersAsync(userManager, logger);

            // Seed Medical Services
            await SeedMedicalServicesAsync(context, logger);

            // Seed Doctors
            await SeedDoctorsAsync(context, userManager, logger);

            // Seed Site Settings
            await SeedSiteSettingsAsync(context, logger);

            // Seed Content
            await SeedContentAsync(context, logger);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager, ILogger? logger)
    {
        foreach (var roleName in Roles.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new ApplicationRole(roleName, Roles.RoleDescriptions[roleName]);
                var result = await roleManager.CreateAsync(role);
                
                if (result.Succeeded)
                {
                    logger?.LogInformation($"Role '{roleName}' created successfully");
                }
                else
                {
                    logger?.LogError($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, ILogger? logger)
    {
        // Create Super Admin
        var superAdmin = await userManager.FindByEmailAsync("admin@gmail.com");
        if (superAdmin == null)
        {
            superAdmin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Super",
                LastName = "Admin",
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(superAdmin, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(superAdmin, Roles.SuperAdmin);
                logger?.LogInformation("Super Admin user created successfully");
            }
            else
            {
                logger?.LogError($"Failed to create Super Admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        // Create Doctor User
        var doctorUser = await userManager.FindByEmailAsync("dr.smith@gmail.com");
        if (doctorUser == null)
        {
            doctorUser = new ApplicationUser
            {
                UserName = "dr.smith@gmail.com",
                Email = "dr.smith@gmail.com",
                FirstName = "John",
                LastName = "Smith",
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(doctorUser, "Doctor@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(doctorUser, Roles.Doctor);
                logger?.LogInformation("Doctor user created successfully");
            }
        }

        // Create Staff User
        var staffUser = await userManager.FindByEmailAsync("staff@gmail.com");
        if (staffUser == null)
        {
            staffUser = new ApplicationUser
            {
                UserName = "staff@gmail.com",
                Email = "staff@gmail.com",
                FirstName = "Jane",
                LastName = "Doe",
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(staffUser, "Staff@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(staffUser, Roles.Staff);
                logger?.LogInformation("Staff user created successfully");
            }
        }
    }

    private static async Task SeedMedicalServicesAsync(ApplicationDbContext context, ILogger? logger)
    {
        if (!await context.MedicalServices.AnyAsync())
        {
            var services = new List<MedicalService>
            {
                new MedicalService
                {
                    ServiceName = "Dental Implants",
                    ServiceNameAr = "زراعة الأسنان",
                    Description = "Advanced dental implant procedures for missing teeth replacement",
                    DescriptionAr = "إجراءات متقدمة لزراعة الأسنان لاستبدال الأسنان المفقودة",
                    Category = "Dental",
                    EstimatedDuration = "120",
                    PriceRange = "$800 - $2,500",
                    IsActive = true,
                    DisplayOrder = 1,
                    Icon = "default-icon.png"
                },
                new MedicalService
                {
                    ServiceName = "Teeth Whitening",
                    ServiceNameAr = "تبييض الأسنان",
                    Description = "Professional teeth whitening treatments for a brighter smile",
                    DescriptionAr = "علاجات تبييض الأسنان المهنية للحصول على ابتسامة أكثر إشراقاً",
                    Category = "Dental",
                    EstimatedDuration = "60",
                    PriceRange = "$200 - $500",
                    IsActive = true,
                    DisplayOrder = 2,
                    Icon = "default-icon.png"
                },
                new MedicalService
                {
                    ServiceName = "Hair Transplant",
                    ServiceNameAr = "زراعة الشعر",
                    Description = "Advanced hair transplant procedures using FUE technique",
                    DescriptionAr = "إجراءات زراعة الشعر المتقدمة باستخدام تقنية FUE",
                    Category = "Hair Restoration",
                   EstimatedDuration = "480",
                    PriceRange = "$2,000 - $8,000",
                    IsActive = true,
                    DisplayOrder = 3,
                    Icon = "default-icon.png"
                },
                new MedicalService
                {
                    ServiceName = "Veneers",
                    ServiceNameAr = "القشور التجميلية",
                    Description = "Porcelain veneers for perfect smile makeover",
                    DescriptionAr = "القشور الخزفية لتجميل الابتسامة المثالية",
                    Category = "Dental",
                    EstimatedDuration = "90",
                    PriceRange = "$500 - $1,200",
                    IsActive = true,
                    DisplayOrder = 4,
                    Icon = "default-icon.png"
                },
                new MedicalService
                {
                    ServiceName = "All-on-4 Treatment",
                    ServiceNameAr = "علاج الكل على 4",
                    Description = "Complete mouth restoration with All-on-4 implant technique",
                    DescriptionAr = "ترميم الفم الكامل بتقنية زراعة الكل على 4",
                    Category = "Dental",
                    EstimatedDuration = "240",
                    PriceRange = "$8,000 - $15,000",
                    IsActive = true,
                    DisplayOrder = 5,
                    Icon = "default-icon.png"
                }
            };

            await context.MedicalServices.AddRangeAsync(services);
            logger?.LogInformation("Medical services seeded successfully");
        }
    }

    private static async Task SeedDoctorsAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger? logger)
    {
        if (!await context.Doctors.AnyAsync())
        {
            var doctorUser = await userManager.FindByEmailAsync("dr.smith@apolloaesthetics.com");
            
            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    FullName = "Dr. John Smith",
                    Title = "Dr.",
                    Specialization = "Oral and Maxillofacial Surgery",
                    Biography = "Dr. Smith has over 15 years of experience in dental implants and oral surgery.",
                    BiographyAr = "د. سميث لديه أكثر من 15 عاماً من الخبرة في زراعة الأسنان وجراحة الفم.",
                    Education = "DDS from Harvard School of Dental Medicine",
                    Experience = "15+ years",
                    Languages = "English, Arabic, Turkish",
                    Phone = "+90 242 753 43 41",
                    Email = "dr.smith@apolloaesthetics.com",
                    IsActive = true,
                    DisplayOrder = 1,
                    UserId = doctorUser?.Id
                },
                new Doctor
                {
                    FullName = "Dr. Sarah Johnson",
                    Title = "Dr.",
                    Specialization = "Cosmetic Dentistry",
                    Biography = "Dr. Johnson specializes in cosmetic dentistry and smile makeovers.",
                    BiographyAr = "د. جونسون متخصصة في طب الأسنان التجميلي وتجميل الابتسامة.",
                    Education = "DDS from UCLA School of Dentistry",
                    Experience = "12+ years",
                    Languages = "English, Spanish",
                    Phone = "+90 242 753 43 42",
                    Email = "dr.johnson@apolloaesthetics.com",
                    IsActive = true,
                    DisplayOrder = 2
                },
                new Doctor
                {
                    FullName = "Dr. Ahmed Hassan",
                    Title = "Dr.",
                    Specialization = "Hair Transplant Surgery",
                    Biography = "Dr. Hassan is a leading expert in hair restoration and FUE techniques.",
                    BiographyAr = "د. حسن خبير رائد في استعادة الشعر وتقنيات FUE.",
                    Education = "MD from Istanbul University",
                    Experience = "10+ years",
                    Languages = "Arabic, Turkish, English",
                    Phone = "+90 242 753 43 43",
                    Email = "dr.hassan@apolloaesthetics.com",
                    IsActive = true,
                    DisplayOrder = 3
                }
            };

            await context.Doctors.AddRangeAsync(doctors);
            logger?.LogInformation("Doctors seeded successfully");
        }
    }

    private static async Task SeedSiteSettingsAsync(ApplicationDbContext context, ILogger? logger)
    {
        if (!await context.SiteSettings.AnyAsync())
        {
            var settings = new List<SiteSetting>
            {
                new SiteSetting { Key = "SiteName", Value = "Apollo Aesthetics", Category = "General", DataType = "string" },
                new SiteSetting { Key = "SiteDescription", Value = "Your trusted partner for dental care and aesthetic treatments", Category = "General", DataType = "string" },
                new SiteSetting { Key = "ContactEmail", Value = "info@apolloaesthetics.com", Category = "Contact", DataType = "string" },
                new SiteSetting { Key = "ContactPhone", Value = "+90 242 753 43 41", Category = "Contact", DataType = "string" },
                new SiteSetting { Key = "Address", Value = "Side/Antalya, Turkey", Category = "Contact", DataType = "string" },
                new SiteSetting { Key = "WorkingHours", Value = "Mon-Fri: 9:00 AM - 6:00 PM", Category = "Contact", DataType = "string" },
                new SiteSetting { Key = "FacebookUrl", Value = "https://facebook.com/apolloaesthetics", Category = "Social", DataType = "string" },
                new SiteSetting { Key = "InstagramUrl", Value = "https://instagram.com/apolloaesthetics", Category = "Social", DataType = "string" },
                new SiteSetting { Key = "WhatsAppNumber", Value = "+90 242 753 43 41", Category = "Social", DataType = "string" }
            };

            await context.SiteSettings.AddRangeAsync(settings);
            logger?.LogInformation("Site settings seeded successfully");
        }
    }

    private static async Task SeedContentAsync(ApplicationDbContext context, ILogger? logger)
    {
        if (!await context.Contents.AnyAsync())
        {
            var contents = new List<Content>
            {
                new Content
                {
                    Key = "HomePage_Hero_Title",
                    Title = "Welcome to Apollo Aesthetics",
                    TitleAr = "مرحباً بكم في أبولو للتجميل",
                    Body = "Your trusted partner for dental care and aesthetic treatments in Turkey",
                    BodyAr = "شريكك الموثوق للعناية بالأسنان والعلاجات التجميلية في تركيا",
                    ContentType = "Hero",
                    IsPublished = true
                },
                new Content
                {
                    Key = "About_Description",
                    Title = "About Apollo Aesthetics",
                    TitleAr = "حول أبولو للتجميل",
                    Body = "Apollo Aesthetics is a leading medical tourism destination in Turkey, offering world-class dental and aesthetic treatments with experienced doctors and modern facilities.",
                    BodyAr = "أبولو للتجميل هو وجهة رائدة للسياحة العلاجية في تركيا، يقدم علاجات الأسنان والتجميل عالمية المستوى مع أطباء ذوي خبرة ومرافق حديثة.",
                    ContentType = "About",
                    IsPublished = true
                }
            };

            await context.Contents.AddRangeAsync(contents);
            logger?.LogInformation("Content seeded successfully");
        }
    }
}

