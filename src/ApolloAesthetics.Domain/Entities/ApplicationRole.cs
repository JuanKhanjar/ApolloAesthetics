using Microsoft.AspNetCore.Identity;

namespace ApolloAesthetics.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
        Name = roleName;
        NormalizedName = roleName.ToUpper();
    }

    public ApplicationRole(string roleName, string description) : this(roleName)
    {
        Description = description;
    }
}

