using ApolloAesthetics.Application.DTOs;
using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Application.Mappings;

public static class AppointmentMappings
{
    public static AppointmentDto ToDto(this Appointment appointment)
    {
        return new AppointmentDto
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            ServiceId = appointment.ServiceId,
            AppointmentDate = appointment.AppointmentDate,
            Duration = appointment.Duration,
            Status = appointment.Status,
            Notes = appointment.Notes,
            PatientNotes = appointment.PatientNotes,
            DoctorNotes = appointment.DoctorNotes,
            TreatmentPlan = appointment.TreatmentPlan,
            EstimatedCost = appointment.EstimatedCost,
            ActualCost = appointment.ActualCost,
            PatientName = appointment.Patient?.FullName ?? "",
            DoctorName = appointment.Doctor?.FullName ?? "",
            ServiceName = appointment.Service?.ServiceName ?? "",
            PatientPhone = appointment.Patient?.Phone ?? "",
            PatientEmail = appointment.Patient?.Email ?? ""
        };
    }

    public static Appointment ToEntity(this CreateAppointmentDto createDto)
    {
        return new Appointment
        {
            PatientId = createDto.PatientId,
            DoctorId = createDto.DoctorId,
            ServiceId = createDto.ServiceId,
            AppointmentDate = createDto.AppointmentDate,
            Duration = createDto.Duration,
            Notes = createDto.Notes,
            PatientNotes = createDto.PatientNotes,
            EstimatedCost = createDto.EstimatedCost
        };
    }

    public static void UpdateEntity(this UpdateAppointmentDto updateDto, Appointment appointment)
    {
        appointment.AppointmentDate = updateDto.AppointmentDate;
        appointment.Duration = updateDto.Duration;
        appointment.Status = updateDto.Status;
        appointment.Notes = updateDto.Notes;
        appointment.DoctorNotes = updateDto.DoctorNotes;
        appointment.TreatmentPlan = updateDto.TreatmentPlan;
        appointment.ActualCost = updateDto.ActualCost;
    }
}

