using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<MedicalService> MedicalServices { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<ConsultationRequest> ConsultationRequests { get; set; }
    public DbSet<GalleryImage> GalleryImages { get; set; }
    public DbSet<BeforeAfterImage> BeforeAfterImages { get; set; }
    public DbSet<DoctorService> DoctorServices { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<SiteSetting> SiteSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Identity tables with custom names
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.ProfilePicture).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        });

        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("Roles");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
        });

        // Configure Doctor
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(200);
            entity.Property(e => e.UserId).HasMaxLength(450);
            
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.HasOne(e => e.User)
                  .WithOne(u => u.Doctor)
                  .HasForeignKey<Doctor>(e => e.UserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure MedicalService
        modelBuilder.Entity<MedicalService>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ServiceName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ServiceNameAr).HasMaxLength(200);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.PriceRange).HasMaxLength(100);
        });

        // Configure Patient
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserId).HasMaxLength(450);
            
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserId);
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Patients)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Appointment
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(e => e.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Doctor)
                  .WithMany(d => d.Appointments)
                  .HasForeignKey(e => e.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Service)
                  .WithMany(s => s.Appointments)
                  .HasForeignKey(e => e.ServiceId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.EstimatedCost).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ActualCost).HasColumnType("decimal(18,2)");
        });

        // Configure ConsultationRequest
        modelBuilder.Entity<ConsultationRequest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        // Configure GalleryImage
        modelBuilder.Entity<GalleryImage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.HasOne(e => e.Service)
                  .WithMany(s => s.GalleryImages)
                  .HasForeignKey(e => e.ServiceId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure BeforeAfterImage
        modelBuilder.Entity<BeforeAfterImage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.BeforeAfterImages)
                  .HasForeignKey(e => e.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Service)
                  .WithMany(s => s.BeforeAfterImages)
                  .HasForeignKey(e => e.ServiceId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure DoctorService (Many-to-Many)
        modelBuilder.Entity<DoctorService>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Doctor)
                  .WithMany(d => d.DoctorServices)
                  .HasForeignKey(e => e.DoctorId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Service)
                  .WithMany(s => s.DoctorServices)
                  .HasForeignKey(e => e.ServiceId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.HasIndex(e => new { e.DoctorId, e.ServiceId }).IsUnique();
        });

        // Configure Testimonial
        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PatientName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PatientLocation).HasMaxLength(200);
            entity.Property(e => e.ServiceReceived).HasMaxLength(200);
            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.Testimonials)
                  .HasForeignKey(e => e.PatientId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Content
        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.TitleAr).HasMaxLength(500);
            entity.Property(e => e.ContentType).HasMaxLength(50);
            entity.HasIndex(e => e.Key).IsUnique();
        });

        // Configure SiteSetting
        modelBuilder.Entity<SiteSetting>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.HasIndex(e => e.Key).IsUnique();
        });

        // Configure soft delete filter
        modelBuilder.Entity<Doctor>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<MedicalService>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Patient>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Appointment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ConsultationRequest>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<GalleryImage>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<BeforeAfterImage>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DoctorService>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Testimonial>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Content>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SiteSetting>().HasQueryFilter(e => !e.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Common.BaseEntity && (
                e.State == EntityState.Added ||
                e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var entity = (Domain.Common.BaseEntity)entityEntry.Entity;
            
            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            
            entity.UpdatedAt = DateTime.UtcNow;
        }

        // Handle ApplicationUser updates
        var userEntries = ChangeTracker.Entries<ApplicationUser>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var userEntry in userEntries)
        {
            if (userEntry.State == EntityState.Modified)
            {
                userEntry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

