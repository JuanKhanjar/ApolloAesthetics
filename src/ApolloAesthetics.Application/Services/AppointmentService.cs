using ApolloAesthetics.Application.DTOs;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Application.Mappings;
using ApolloAesthetics.Domain.Entities;
using ApolloAesthetics.Domain.Enums;
using ApolloAesthetics.Domain.Interfaces;

namespace ApolloAesthetics.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        var appointments = await _unitOfWork.Appointments.GetAllAsync();
        var result = new List<AppointmentDto>();

        foreach (var appointment in appointments)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(appointment.PatientId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(appointment.DoctorId);
            var service = await _unitOfWork.MedicalServices.GetByIdAsync(appointment.ServiceId);

            var dto = appointment.ToDto();
            dto.PatientName = patient?.FullName ?? "";
            dto.DoctorName = doctor?.FullName ?? "";
            dto.ServiceName = service?.ServiceName ?? "";
            dto.PatientPhone = patient?.Phone ?? "";
            dto.PatientEmail = patient?.Email ?? "";

            result.Add(dto);
        }

        return result.OrderBy(a => a.AppointmentDate);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDateAsync(DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        var appointments = await _unitOfWork.Appointments.FindAsync(a => 
            a.AppointmentDate >= startDate && a.AppointmentDate < endDate);

        var result = new List<AppointmentDto>();

        foreach (var appointment in appointments)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(appointment.PatientId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(appointment.DoctorId);
            var service = await _unitOfWork.MedicalServices.GetByIdAsync(appointment.ServiceId);

            var dto = appointment.ToDto();
            dto.PatientName = patient?.FullName ?? "";
            dto.DoctorName = doctor?.FullName ?? "";
            dto.ServiceName = service?.ServiceName ?? "";
            dto.PatientPhone = patient?.Phone ?? "";
            dto.PatientEmail = patient?.Email ?? "";

            result.Add(dto);
        }

        return result.OrderBy(a => a.AppointmentDate);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId)
    {
        var appointments = await _unitOfWork.Appointments.FindAsync(a => a.DoctorId == doctorId);
        return await ProcessAppointments(appointments);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId)
    {
        var appointments = await _unitOfWork.Appointments.FindAsync(a => a.PatientId == patientId);
        var result = await ProcessAppointments(appointments);
        return result.OrderByDescending(a => a.AppointmentDate);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByStatusAsync(AppointmentStatus status)
    {
        var appointments = await _unitOfWork.Appointments.FindAsync(a => a.Status == status);
        return await ProcessAppointments(appointments);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) return null;

        var patient = await _unitOfWork.Patients.GetByIdAsync(appointment.PatientId);
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(appointment.DoctorId);
        var service = await _unitOfWork.MedicalServices.GetByIdAsync(appointment.ServiceId);

        var dto = appointment.ToDto();
        dto.PatientName = patient?.FullName ?? "";
        dto.DoctorName = doctor?.FullName ?? "";
        dto.ServiceName = service?.ServiceName ?? "";
        dto.PatientPhone = patient?.Phone ?? "";
        dto.PatientEmail = patient?.Email ?? "";

        return dto;
    }

    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createDto)
    {
        var appointment = createDto.ToEntity();
        appointment.Status = AppointmentStatus.Pending;

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return await GetAppointmentByIdAsync(appointment.Id) ?? new AppointmentDto();
    }

    public async Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto updateDto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(updateDto.Id);
        if (appointment == null)
            throw new ArgumentException("Appointment not found");

        updateDto.UpdateEntity(appointment);
        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return await GetAppointmentByIdAsync(appointment.Id) ?? new AppointmentDto();
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        await _unitOfWork.Appointments.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CancelAppointmentAsync(int id, string reason)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) return false;

        appointment.Status = AppointmentStatus.Cancelled;
        appointment.Notes += $"\nCancelled: {reason}";

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CompleteAppointmentAsync(int id, string notes, decimal? actualCost)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) return false;

        appointment.Status = AppointmentStatus.Completed;
        appointment.DoctorNotes = notes;
        appointment.ActualCost = actualCost;

        await _unitOfWork.Appointments.UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AppointmentDto>> GetTodayAppointmentsAsync()
    {
        return await GetAppointmentsByDateAsync(DateTime.Today);
    }

    public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(int days = 7)
    {
        var startDate = DateTime.Today;
        var endDate = startDate.AddDays(days);

        var appointments = await _unitOfWork.Appointments.FindAsync(a => 
            a.AppointmentDate >= startDate && a.AppointmentDate <= endDate &&
            a.Status != AppointmentStatus.Cancelled);

        return await ProcessAppointments(appointments);
    }

    public async Task<bool> IsTimeSlotAvailableAsync(int doctorId, DateTime appointmentDate, int duration)
    {
        var endTime = appointmentDate.AddMinutes(duration);
        
        var conflictingAppointments = await _unitOfWork.Appointments.FindAsync(a =>
            a.DoctorId == doctorId &&
            a.Status != AppointmentStatus.Cancelled &&
            ((a.AppointmentDate <= appointmentDate && a.AppointmentDate.AddMinutes(a.Duration) > appointmentDate) ||
             (a.AppointmentDate < endTime && a.AppointmentDate >= appointmentDate)));

        return !conflictingAppointments.Any();
    }

    public async Task<Dictionary<string, int>> GetAppointmentStatsAsync()
    {
        var today = DateTime.Today;
        var thisMonth = new DateTime(today.Year, today.Month, 1);

        var todayCount = await _unitOfWork.Appointments.CountAsync(a => 
            a.AppointmentDate.Date == today);

        var thisMonthCount = await _unitOfWork.Appointments.CountAsync(a => 
            a.AppointmentDate >= thisMonth);

        var pendingCount = await _unitOfWork.Appointments.CountAsync(a => 
            a.Status == AppointmentStatus.Pending);

        var completedCount = await _unitOfWork.Appointments.CountAsync(a => 
            a.Status == AppointmentStatus.Completed);

        return new Dictionary<string, int>
        {
            { "Today", todayCount },
            { "ThisMonth", thisMonthCount },
            { "Pending", pendingCount },
            { "Completed", completedCount }
        };
    }

    private async Task<IEnumerable<AppointmentDto>> ProcessAppointments(IEnumerable<Appointment> appointments)
    {
        var result = new List<AppointmentDto>();

        foreach (var appointment in appointments)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(appointment.PatientId);
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(appointment.DoctorId);
            var service = await _unitOfWork.MedicalServices.GetByIdAsync(appointment.ServiceId);

            var dto = appointment.ToDto();
            dto.PatientName = patient?.FullName ?? "";
            dto.DoctorName = doctor?.FullName ?? "";
            dto.ServiceName = service?.ServiceName ?? "";
            dto.PatientPhone = patient?.Phone ?? "";
            dto.PatientEmail = patient?.Email ?? "";

            result.Add(dto);
        }

        return result.OrderBy(a => a.AppointmentDate);
    }

    public Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(string patientId)
    {
        throw new NotImplementedException( );
    }

    public Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId)
    {
        throw new NotImplementedException( );
    }

    public Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<bool> ConfirmAppointmentAsync(int id)
    {
        throw new NotImplementedException( );
    }

    public Task<bool> CancelAppointmentAsync(int id)
    {
        throw new NotImplementedException( );
    }

    public Task<bool> CompleteAppointmentAsync(int id)
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetTotalAppointmentsAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetTodayAppointmentsCountAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetThisMonthAppointmentsCountAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetCompletedAppointmentsCountAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetPendingAppointmentsCountAsync()
    {
        throw new NotImplementedException( );
    }

    public Task<int> GetCancelledAppointmentsCountAsync()
    {
        throw new NotImplementedException( );
    }
}

