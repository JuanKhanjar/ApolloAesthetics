using ApolloAesthetics.Application.DTOs;

namespace ApolloAesthetics.Application.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
    Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
    Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto appointmentDto);
    Task<bool> DeleteAppointmentAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientIdAsync(string patientId);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByDateAsync(DateTime date);
    Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync();
    Task<IEnumerable<AppointmentDto>> GetTodayAppointmentsAsync();
    Task<bool> ConfirmAppointmentAsync(int id);
    Task<bool> CancelAppointmentAsync(int id);
    Task<bool> CompleteAppointmentAsync(int id);
    
    // Statistics methods
    Task<int> GetTotalAppointmentsAsync();
    Task<int> GetTodayAppointmentsCountAsync();
    Task<int> GetThisMonthAppointmentsCountAsync();
    Task<int> GetCompletedAppointmentsCountAsync();
    Task<int> GetPendingAppointmentsCountAsync();
    Task<int> GetCancelledAppointmentsCountAsync();
}

