using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class MedicalService : BaseEntity
{
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceNameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string EstimatedDuration { get; set; } = string.Empty;
    public string PriceRange { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    // Navigation Properties
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>( );
    public virtual ICollection<DoctorService> DoctorServices { get; set; } = new List<DoctorService>( );
    public virtual ICollection<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>( );
    public virtual ICollection<BeforeAfterImage> BeforeAfterImages { get; set; } = new List<BeforeAfterImage>( );
    public decimal Price { get; set; }
    public string Icon { get; set; }
}

