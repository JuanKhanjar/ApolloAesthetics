using Microsoft.EntityFrameworkCore.Storage;
using ApolloAesthetics.Domain.Entities;
using ApolloAesthetics.Domain.Interfaces;
using ApolloAesthetics.Infrastructure.Data;

namespace ApolloAesthetics.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        
        Doctors = new Repository<Doctor>(_context);
        MedicalServices = new Repository<MedicalService>(_context);
        Patients = new Repository<Patient>(_context);
        Appointments = new Repository<Appointment>(_context);
        ConsultationRequests = new Repository<ConsultationRequest>(_context);
        GalleryImages = new Repository<GalleryImage>(_context);
        BeforeAfterImages = new Repository<BeforeAfterImage>(_context);
        DoctorServices = new Repository<DoctorService>(_context);
        Testimonials = new Repository<Testimonial>(_context);
        Contents = new Repository<Content>(_context);
        SiteSettings = new Repository<SiteSetting>(_context);
    }

    public IRepository<Doctor> Doctors { get; }
    public IRepository<MedicalService> MedicalServices { get; }
    public IRepository<Patient> Patients { get; }
    public IRepository<Appointment> Appointments { get; }
    public IRepository<ConsultationRequest> ConsultationRequests { get; }
    public IRepository<GalleryImage> GalleryImages { get; }
    public IRepository<BeforeAfterImage> BeforeAfterImages { get; }
    public IRepository<DoctorService> DoctorServices { get; }
    public IRepository<Testimonial> Testimonials { get; }
    public IRepository<Content> Contents { get; }
    public IRepository<SiteSetting> SiteSettings { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}

