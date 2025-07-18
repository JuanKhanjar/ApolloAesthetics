using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class GalleryImage : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string ThumbnailPath { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int? ServiceId { get; set; }
    public bool IsBeforeAfter { get; set; } = false;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual MedicalService? Service { get; set; }
}

