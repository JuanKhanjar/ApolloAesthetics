using ApolloAesthetics.Domain.Enums;

namespace ApolloAesthetics.Application.DTOs;

public class PatientDto
{
    public int Id { get; set; } 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string MedicalHistory { get; set; } = string.Empty;
    public string Allergies { get; set; } = string.Empty;
    public string EmergencyContact { get; set; } = string.Empty;
    public string EmergencyPhone { get; set; } = string.Empty;
    public string InsuranceInfo { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastAppointment { get; set; }
    public int TotalAppointments { get; set; } = 0;

    // Computed properties
    public string FullName => $"{FirstName} {LastName}";
    public int? Age => DateOfBirth.HasValue ? DateTime.Now.Year - DateOfBirth.Value.Year : null;
    public string GenderDisplay => Gender.ToString();
}

public class CreatePatientDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string EmergencyContact { get; set; } = string.Empty;
    public string MedicalHistory { get; set; } = string.Empty;
    public string Allergies { get; set; } = string.Empty;
}

public class UpdatePatientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string EmergencyContact { get; set; } = string.Empty;
    public string MedicalHistory { get; set; } = string.Empty;
    public string Allergies { get; set; } = string.Empty;
}

