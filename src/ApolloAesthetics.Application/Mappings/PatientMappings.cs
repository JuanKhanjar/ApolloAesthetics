using ApolloAesthetics.Application.DTOs;
using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Application.Mappings;

public static class PatientMappings
{
    public static PatientDto ToDto(this Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.Email,
            PhoneNumber = patient.Phone,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            Address = patient.Address,
            City = patient.City,
            Country = patient.Country,
            EmergencyContact = patient.EmergencyContact,
            MedicalHistory = patient.MedicalHistory,
            Allergies = patient.Allergies,
            CreatedAt = patient.CreatedAt,
            TotalAppointments = patient.Appointments?.Count ?? 0,
            LastAppointment = patient.Appointments?.OrderByDescending(a => a.AppointmentDate).FirstOrDefault()?.AppointmentDate
        };
    }

    public static Patient ToEntity(this CreatePatientDto createDto)
    {
        return new Patient
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            Phone = createDto.Phone,
            DateOfBirth = createDto.DateOfBirth,
            Gender = createDto.Gender.Value,
            Address = createDto.Address,
            City = createDto.City,
            Country = createDto.Country,
            EmergencyContact = createDto.EmergencyContact,
            MedicalHistory = createDto.MedicalHistory,
            Allergies = createDto.Allergies
        };
    }

    public static void UpdateEntity(this UpdatePatientDto updateDto, Patient patient)
    {
        patient.FirstName = updateDto.FirstName;
        patient.LastName = updateDto.LastName;
        patient.Email = updateDto.Email;
        patient.Phone = updateDto.Phone;
        patient.DateOfBirth = updateDto.DateOfBirth;
        patient.Gender = updateDto.Gender.Value;
        patient.Address = updateDto.Address;
        patient.City = updateDto.City;
        patient.Country = updateDto.Country;
        patient.EmergencyContact = updateDto.EmergencyContact;
        patient.MedicalHistory = updateDto.MedicalHistory;
        patient.Allergies = updateDto.Allergies;
    }
}

