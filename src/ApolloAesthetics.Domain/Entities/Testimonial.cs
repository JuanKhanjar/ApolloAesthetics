using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class Testimonial : BaseEntity
{
    public int? PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string PatientLocation { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ContentAr { get; set; } = string.Empty;
    public int Rating { get; set; } = 5; // 1-5 stars
    public string ServiceReceived { get; set; } = string.Empty;
    public DateTime TreatmentDate { get; set; }
    public bool IsApproved { get; set; } = false;
    public bool IsPublic { get; set; } = true;
    public int DisplayOrder { get; set; }

    // Navigation Properties
    public virtual Patient? Patient { get; set; }
}

