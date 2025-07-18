using ApolloAesthetics.Domain.Enums;

namespace ApolloAesthetics.Application.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Duration { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string PatientNotes { get; set; } = string.Empty;
    public string DoctorNotes { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
    public decimal? EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }

    // Navigation properties
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;
    public string PatientEmail { get; set; } = string.Empty;
}

public class CreateAppointmentDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Duration { get; set; } = 60;
    public string Notes { get; set; } = string.Empty;
    public string PatientNotes { get; set; } = string.Empty;
    public decimal? EstimatedCost { get; set; }
}

public class UpdateAppointmentDto
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Duration { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string DoctorNotes { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
    public decimal? ActualCost { get; set; }
}

