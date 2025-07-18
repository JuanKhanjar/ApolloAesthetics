using ApolloAesthetics.Domain.Common;
using ApolloAesthetics.Domain.Enums;

namespace ApolloAesthetics.Domain.Entities;

public class ConsultationRequest : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ServiceOfInterest { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ContactMethod PreferredContactMethod { get; set; } = ContactMethod.Email;
    public ConsultationStatus Status { get; set; } = ConsultationStatus.New;
    public Priority Priority { get; set; } = Priority.Normal;
    public string AssignedToUserId { get; set; } = string.Empty;
    public string ResponseMessage { get; set; } = string.Empty;
    public DateTime? ResponseDate { get; set; }
}

