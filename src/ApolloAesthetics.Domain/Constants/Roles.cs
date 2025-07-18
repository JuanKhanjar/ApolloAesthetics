namespace ApolloAesthetics.Domain.Constants;

public static class Roles
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Admin";
    public const string Doctor = "Doctor";
    public const string Staff = "Staff";
    public const string Patient = "Patient";

    public static readonly string[] AllRoles = 
    {
        SuperAdmin,
        Admin,
        Doctor,
        Staff,
        Patient
    };

    public static readonly Dictionary<string, string> RoleDescriptions = new()
    {
        { SuperAdmin, "Super Administrator with full system access" },
        { Admin, "Administrator with management access" },
        { Doctor, "Medical doctor with patient and appointment access" },
        { Staff, "Staff member with limited access" },
        { Patient, "Patient with personal data access" }
    };
}

