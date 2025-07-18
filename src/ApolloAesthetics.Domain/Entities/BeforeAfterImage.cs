using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class BeforeAfterImage : BaseEntity
{
    public int PatientId { get; set; }
    public int ServiceId { get; set; }
    public string BeforeImagePath { get; set; } = string.Empty;
    public string AfterImagePath { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime TreatmentDate { get; set; }
    public bool IsPublic { get; set; } = false;
    public bool PatientConsent { get; set; } = false;
    public int DisplayOrder { get; set; }

    // Navigation Properties
    public virtual Patient Patient { get; set; } = null!;
    public virtual MedicalService Service { get; set; } = null!;
}

