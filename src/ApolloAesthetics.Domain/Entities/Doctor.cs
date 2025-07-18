using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class Doctor : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty; // Dr., Prof., etc.
    public string Specialization { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public string BiographyAr { get; set; } = string.Empty;
    public string Education { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public string? UserId { get; set; } // Foreign key to ApplicationUser

    // Navigation properties
    public virtual ApplicationUser? User { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public int YearsOfExperience { get; set; }
}

