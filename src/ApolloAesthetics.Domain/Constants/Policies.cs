namespace ApolloAesthetics.Domain.Constants;

public static class Policies
{
    // Admin Policies
    public const string AdminOnly = "AdminOnly";
    public const string AdminOrSuperAdmin = "AdminOrSuperAdmin";
    
    // Doctor Policies
    public const string DoctorOnly = "DoctorOnly";
    public const string DoctorOrAdmin = "DoctorOrAdmin";
    
    // Staff Policies
    public const string StaffOrAbove = "StaffOrAbove";
    public const string MedicalStaff = "MedicalStaff";
    
    // Patient Policies
    public const string PatientOnly = "PatientOnly";
    public const string PatientOrMedicalStaff = "PatientOrMedicalStaff";
    
    // General Policies
    public const string AuthenticatedUser = "AuthenticatedUser";
    public const string ManageAppointments = "ManageAppointments";
    public const string ManagePatients = "ManagePatients";
    public const string ManageConsultations = "ManageConsultations";
    public const string ViewReports = "ViewReports";
    public const string ManageSystem = "ManageSystem";
}

