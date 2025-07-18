using ApolloAesthetics.Application.DTOs;

namespace ApolloAesthetics.Web.Models.Admin;

public class DashboardViewModel
{
    public int TodayAppointments { get; set; }
    public int ThisMonthAppointments { get; set; }
    public int PendingAppointments { get; set; }
    public int CompletedAppointments { get; set; }
    public int TotalPatients { get; set; }
    public int NewPatientsThisMonth { get; set; }
    public int NewConsultations { get; set; }
    public int TotalConsultations { get; set; }

    public IEnumerable<AppointmentDto> TodayAppointmentsList { get; set; } = new List<AppointmentDto>();
    public IEnumerable<PatientDto> RecentPatients { get; set; } = new List<PatientDto>();
    public IEnumerable<ConsultationRequestDto> NewConsultationRequests { get; set; } = new List<ConsultationRequestDto>();
    public int TotalAppointments { get; internal set; }
    public int TotalUsers { get; internal set; }
    public int ActiveUsers { get; internal set; }
}

