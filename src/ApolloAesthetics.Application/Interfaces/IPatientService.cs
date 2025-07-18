using ApolloAesthetics.Application.DTOs;

namespace ApolloAesthetics.Application.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto?> GetPatientByIdAsync(string id);
    Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto);
    Task<PatientDto> UpdatePatientAsync(UpdatePatientDto patientDto);
    Task<bool> DeletePatientAsync(string id);
    Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm);
    Task<PatientDto?> GetPatientByEmailAsync(string email);
    Task<PatientDto?> GetPatientByPhoneAsync(string phone);
    Task<IEnumerable<PatientDto>> GetRecentPatientsAsync(int count = 10);
    
    // Statistics methods
    Task<int> GetTotalPatientsAsync();
    Task<int> GetNewPatientsThisMonthAsync();
    Task<int> GetActivePatientsAsync();
}

