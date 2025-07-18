using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Doctor> Doctors { get; }
    IRepository<MedicalService> MedicalServices { get; }
    IRepository<Patient> Patients { get; }
    IRepository<Appointment> Appointments { get; }
    IRepository<ConsultationRequest> ConsultationRequests { get; }
    IRepository<GalleryImage> GalleryImages { get; }
    IRepository<BeforeAfterImage> BeforeAfterImages { get; }
    IRepository<DoctorService> DoctorServices { get; }
    IRepository<Testimonial> Testimonials { get; }
    IRepository<Content> Contents { get; }
    IRepository<SiteSetting> SiteSettings { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}

