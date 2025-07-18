using ApolloAesthetics.Application.DTOs;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Application.Mappings;
using ApolloAesthetics.Domain.Interfaces;

namespace ApolloAesthetics.Application.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await _unitOfWork.Patients.GetAllAsync();
        return patients.Select(p => p.ToDto()).OrderBy(p => p.FirstName);
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);
        return patient?.ToDto();
    }

    public async Task<PatientDto?> GetPatientByEmailAsync(string email)
    {
        var patient = await _unitOfWork.Patients.FirstOrDefaultAsync(p => p.Email == email);
        return patient?.ToDto();
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createDto)
    {
        var patient = createDto.ToEntity();
        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();
        return patient.ToDto();
    }

    public async Task<PatientDto> UpdatePatientAsync(UpdatePatientDto updateDto)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(updateDto.Id);
        if (patient == null)
            throw new ArgumentException("Patient not found");

        updateDto.UpdateEntity(patient);
        await _unitOfWork.Patients.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();
        return patient.ToDto();
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        await _unitOfWork.Patients.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm)
    {
        var patients = await _unitOfWork.Patients.FindAsync(p => 
            p.FirstName.Contains(searchTerm) || 
            p.LastName.Contains(searchTerm) || 
            p.Email.Contains(searchTerm) || 
            p.Phone.Contains(searchTerm));
        
        return patients.Select(p => p.ToDto()).OrderBy(p => p.FirstName);
    }

    public async Task<IEnumerable<PatientDto>> GetRecentPatientsAsync(int count = 10)
    {
        var patients = await _unitOfWork.Patients.GetAllAsync();
        return patients.OrderByDescending(p => p.CreatedAt)
                      .Take(count)
                      .Select(p => p.ToDto());
    }

    public async Task<Dictionary<string, int>> GetPatientStatsAsync()
    {
        var totalPatients = await _unitOfWork.Patients.CountAsync();
        var thisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var newThisMonth = await _unitOfWork.Patients.CountAsync(p => p.CreatedAt >= thisMonth);

        return new Dictionary<string, int>
        {
            { "Total", totalPatients },
            { "NewThisMonth", newThisMonth }
        };
    }

    public async Task<bool> PatientExistsAsync(string email)
    {
        return await _unitOfWork.Patients.ExistsAsync(p => p.Email == email);
    }

    public Task<PatientDto?> GetPatientByIdAsync(string id)
    {
        throw new NotImplementedException( );
    }

    public Task<bool> DeletePatientAsync(string id)
    {
        throw new NotImplementedException( );
    }

    public Task<PatientDto?> GetPatientByPhoneAsync(string phone)
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetTotalPatientsAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetNewPatientsThisMonthAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetActivePatientsAsync()
    {
        throw new NotImplementedException( );
    }
}

