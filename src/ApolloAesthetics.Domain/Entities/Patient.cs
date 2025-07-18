using ApolloAesthetics.Domain.Common;
using ApolloAesthetics.Domain.Enums;

namespace ApolloAesthetics.Domain.Entities;

public class Patient : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string EmergencyContact { get; set; } = string.Empty;
    public string EmergencyPhone { get; set; } = string.Empty;
    public string MedicalHistory { get; set; } = string.Empty;
    public string Allergies { get; set; } = string.Empty;
    public string CurrentMedications { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? UserId { get; set; } // Foreign key to ApplicationUser

    // Navigation properties
    public virtual ApplicationUser? User { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>( );
    public virtual ICollection<BeforeAfterImage> BeforeAfterImages { get; set; } = new List<BeforeAfterImage>( );
    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>( );
}

