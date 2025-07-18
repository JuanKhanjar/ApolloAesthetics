using ApolloAesthetics.Domain.Enums;

namespace ApolloAesthetics.Application.DTOs;

public class ConsultationRequestDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ServiceOfInterest { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ConsultationStatus Status { get; set; }
    public Priority Priority { get; set; }
    public ContactMethod PreferredContactMethod { get; set; }
    public DateTime? PreferredContactTime { get; set; }
    public string Response { get; set; } = string.Empty;
    public DateTime? ResponseDate { get; set; }
    public string AssignedToUserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string ResponseMessage { get; set; } = string.Empty;

    // Display properties
    public string StatusDisplay => Status.ToString();
    public string PriorityDisplay => Priority.ToString();
    public string ContactMethodDisplay => PreferredContactMethod.ToString();
}

public class CreateConsultationRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ServiceOfInterest { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ContactMethod PreferredContactMethod { get; set; }
    public DateTime? PreferredContactTime { get; set; }
}

public class UpdateConsultationRequestDto
{
    public int Id { get; set; }
    public ConsultationStatus Status { get; set; }
    public Priority Priority { get; set; }
    public string ResponseMessage { get; set; } = string.Empty;
    public string AssignedToUserId { get; set; } = string.Empty;
}

