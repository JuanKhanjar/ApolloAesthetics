using ApolloAesthetics.Application.DTOs;

namespace ApolloAesthetics.Application.Interfaces;

public interface IConsultationService
{
    Task<IEnumerable<ConsultationRequestDto>> GetAllConsultationsAsync();
    Task<ConsultationRequestDto?> GetConsultationByIdAsync(int id);
    Task<ConsultationRequestDto> CreateConsultationAsync(CreateConsultationRequestDto consultationDto);
    Task<ConsultationRequestDto> UpdateConsultationAsync(UpdateConsultationRequestDto consultationDto);
    Task<bool> DeleteConsultationAsync(int id);
    Task<IEnumerable<ConsultationRequestDto>> GetConsultationsByStatusAsync(int status);
    Task<IEnumerable<ConsultationRequestDto>> GetNewConsultationsAsync();
    Task<bool> RespondToConsultationAsync(int id, string response);
    Task<bool> CloseConsultationAsync(int id);
    
    // Statistics methods
    Task<int> GetTotalConsultationsAsync();
    Task<int> GetNewConsultationsCountAsync();
    Task<int> GetPendingConsultationsCountAsync();
    Task<int> GetRespondedConsultationsCountAsync();
}

