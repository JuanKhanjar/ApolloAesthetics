using ApolloAesthetics.Domain.Common;
using ApolloAesthetics.Domain.Enums;

namespace ApolloAesthetics.Domain.Entities;

public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ServiceId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Duration { get; set; } // in minutes
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string Notes { get; set; } = string.Empty;
    public string PatientNotes { get; set; } = string.Empty;
    public string DoctorNotes { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
    public decimal? EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }

    // Navigation Properties
    public virtual Patient Patient { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;
    public virtual MedicalService Service { get; set; } = null!;
}

