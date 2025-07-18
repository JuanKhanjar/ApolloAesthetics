using ApolloAesthetics.Application.DTOs;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Domain.Entities;
using ApolloAesthetics.Domain.Enums;
using ApolloAesthetics.Domain.Interfaces;

namespace ApolloAesthetics.Application.Services;

public class ConsultationService : IConsultationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ConsultationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ConsultationRequestDto>> GetAllConsultationRequestsAsync()
    {
        var requests = await _unitOfWork.ConsultationRequests.GetAllAsync();
        return requests.Select(ToDto).OrderByDescending(r => r.CreatedAt);
    }

    public async Task<ConsultationRequestDto?> GetConsultationRequestByIdAsync(int id)
    {
        var request = await _unitOfWork.ConsultationRequests.GetByIdAsync(id);
        return request != null ? ToDto(request) : null;
    }

    public async Task<ConsultationRequestDto> CreateConsultationRequestAsync(CreateConsultationRequestDto createDto)
    {
        var request = new ConsultationRequest
        {
            FullName = createDto.FullName,
            Email = createDto.Email,
            Phone = createDto.Phone,
            ServiceOfInterest = createDto.ServiceOfInterest,
            Message = createDto.Message,
            PreferredContactMethod = createDto.PreferredContactMethod,
            Status = ConsultationStatus.New,
            Priority = Priority.Normal
        };

        await _unitOfWork.ConsultationRequests.AddAsync(request);
        await _unitOfWork.SaveChangesAsync();
        return ToDto(request);
    }

    public async Task<ConsultationRequestDto> UpdateConsultationRequestAsync(UpdateConsultationRequestDto updateDto)
    {
        var request = await _unitOfWork.ConsultationRequests.GetByIdAsync(updateDto.Id);
        if (request == null)
            throw new ArgumentException("Consultation request not found");

        request.Status = updateDto.Status;
        request.Priority = updateDto.Priority;
        request.AssignedToUserId = updateDto.AssignedToUserId;
        request.ResponseMessage = updateDto.ResponseMessage;

        if (!string.IsNullOrEmpty(updateDto.ResponseMessage))
        {
            request.ResponseDate = DateTime.UtcNow;
            request.Status = ConsultationStatus.Responded;
        }

        await _unitOfWork.ConsultationRequests.UpdateAsync(request);
        await _unitOfWork.SaveChangesAsync();
        return ToDto(request);
    }

    public async Task<bool> DeleteConsultationRequestAsync(int id)
    {
        await _unitOfWork.ConsultationRequests.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ConsultationRequestDto>> GetConsultationRequestsByStatusAsync(ConsultationStatus status)
    {
        var requests = await _unitOfWork.ConsultationRequests.FindAsync(r => r.Status == status);
        return requests.Select(ToDto).OrderByDescending(r => r.CreatedAt);
    }

    public async Task<IEnumerable<ConsultationRequestDto>> GetNewConsultationRequestsAsync()
    {
        return await GetConsultationRequestsByStatusAsync(ConsultationStatus.New);
    }

    public async Task<bool> AssignConsultationRequestAsync(int id, string userId)
    {
        var request = await _unitOfWork.ConsultationRequests.GetByIdAsync(id);
        if (request == null) return false;

        request.AssignedToUserId = userId;
        request.Status = ConsultationStatus.InProgress;

        await _unitOfWork.ConsultationRequests.UpdateAsync(request);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RespondToConsultationRequestAsync(int id, string response, string userId)
    {
        var request = await _unitOfWork.ConsultationRequests.GetByIdAsync(id);
        if (request == null) return false;

        request.ResponseMessage = response;
        request.ResponseDate = DateTime.UtcNow;
        request.AssignedToUserId = userId;
        request.Status = ConsultationStatus.Responded;

        await _unitOfWork.ConsultationRequests.UpdateAsync(request);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<Dictionary<string, int>> GetConsultationStatsAsync()
    {
        var newCount = await _unitOfWork.ConsultationRequests.CountAsync(r => r.Status == ConsultationStatus.New);
        var inProgressCount = await _unitOfWork.ConsultationRequests.CountAsync(r => r.Status == ConsultationStatus.InProgress);
        var respondedCount = await _unitOfWork.ConsultationRequests.CountAsync(r => r.Status == ConsultationStatus.Responded);
        var totalCount = await _unitOfWork.ConsultationRequests.CountAsync();

        return new Dictionary<string, int>
        {
            { "New", newCount },
            { "InProgress", inProgressCount },
            { "Responded", respondedCount },
            { "Total", totalCount }
        };
    }

    private static ConsultationRequestDto ToDto(ConsultationRequest request)
    {
        return new ConsultationRequestDto
        {
            Id = request.Id,
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            ServiceOfInterest = request.ServiceOfInterest,
            Message = request.Message,
            PreferredContactMethod = request.PreferredContactMethod,
            Status = request.Status,
            Priority = request.Priority,
            AssignedToUserId = request.AssignedToUserId,
            ResponseMessage = request.ResponseMessage,
            ResponseDate = request.ResponseDate,
            CreatedAt = request.CreatedAt
        };
    }

    public Task<IEnumerable<ConsultationRequestDto>> GetAllConsultationsAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<ConsultationRequestDto?> GetConsultationByIdAsync(int id)
    {
        throw new NotImplementedException( );
    }

    public Task<ConsultationRequestDto> CreateConsultationAsync(CreateConsultationRequestDto consultationDto)
    {
        throw new NotImplementedException( );
    }

    public Task<ConsultationRequestDto> UpdateConsultationAsync(UpdateConsultationRequestDto consultationDto)
    {
        throw new NotImplementedException( );
    }

    public Task<bool> DeleteConsultationAsync(int id)
    {
        throw new NotImplementedException( );
    }

    public Task<IEnumerable<ConsultationRequestDto>> GetConsultationsByStatusAsync(int status)
    {
        throw new NotImplementedException( );
    }

    public Task<IEnumerable<ConsultationRequestDto>> GetNewConsultationsAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<bool> RespondToConsultationAsync(int id,string response)
    {
        throw new NotImplementedException( );
    }

    public Task<bool> CloseConsultationAsync(int id)
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetTotalConsultationsAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetNewConsultationsCountAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetPendingConsultationsCountAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetRespondedConsultationsCountAsync()
    {
        throw new NotImplementedException( );
    }
}

