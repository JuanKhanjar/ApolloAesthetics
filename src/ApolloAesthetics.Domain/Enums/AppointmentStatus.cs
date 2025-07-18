namespace ApolloAesthetics.Domain.Enums;

public enum AppointmentStatus
{
    Pending = 1,
    Confirmed = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    NoShow = 6
}

public enum Gender
{
    Male = 1,
    Female = 2
}

public enum ConsultationStatus
{
    New = 1,
    InProgress = 2,
    Responded = 3,
    Closed = 4
}

public enum Priority
{
    Low = 1,
    Normal = 2,
    High = 3,
    Urgent = 4
}

public enum ContactMethod
{
    Email = 1,
    Phone = 2,
    WhatsApp = 3
}

