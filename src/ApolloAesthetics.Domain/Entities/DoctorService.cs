using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class DoctorService : BaseEntity
{
    public int DoctorId { get; set; }
    public int ServiceId { get; set; }
    public decimal? Price { get; set; }
    public string Notes { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual Doctor Doctor { get; set; } = null!;
    public virtual MedicalService Service { get; set; } = null!;
}

