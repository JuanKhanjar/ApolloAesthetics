using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Web.Models;

public class HomeViewModel
{
    public List<Doctor> Doctors { get; set; } = new();
    public List<MedicalService> Services { get; set; } = new();
    public List<Testimonial> Testimonials { get; set; } = new();
    public List<GalleryImage> GalleryImages { get; set; } = new();
    public Dictionary<string, int> Statistics { get; set; } = new();
    
    // Statistics properties for easy access
    public int TotalPatients => Statistics.GetValueOrDefault("TotalPatients", 0);
    public int TotalAppointments => Statistics.GetValueOrDefault("TotalAppointments", 0);
    public int TotalDoctors => Statistics.GetValueOrDefault("TotalDoctors", 0);
    public int TotalServices => Statistics.GetValueOrDefault("TotalServices", 0);
}

public class AboutViewModel
{
    public string Title { get; set; } = "About Apollo Aesthetics";
    public string Description { get; set; } = string.Empty;
    public string Mission { get; set; } = string.Empty;
    public string Vision { get; set; } = string.Empty;
    public List<string> Values { get; set; } = new( );

    public int YearsOfExperience { get; set; }
    public int HappyPatients { get; set; }
    public int Treatments { get; set; }
    public int Awards { get; set; }

    public List<Doctor> Doctors { get; set; } = new( );
    public Dictionary<string,int> Statistics { get; set; } = new( );
}

public class ServicesViewModel
{
    public List<MedicalService> Services { get; set; } = new();
    public List<string> Categories { get; set; } = new();
}

public class GalleryViewModel
{
    public List<GalleryImage> Images { get; set; } = new();
    public List<string> Categories { get; set; } = new() { "All" };
}

public class ContactViewModel
{
    public ContactFormViewModel ContactForm { get; set; } = new();
    public string Address { get; set; } = "Istanbul, Turkey";
    public string Phone { get; set; } = "+90 555 123 4567";
    public string Email { get; set; } = "info@apolloaesthetics.com";
    public string WorkingHours { get; set; } = "Mon-Fri: 9:00 AM - 6:00 PM\nSat: 9:00 AM - 4:00 PM\nSun: Closed";
    public string MapUrl { get; set; } = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3010.2!2d28.9784!3d41.0082!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zNDHCsDAwJzI5LjUiTiAyOMKwNTgnNDIuMiJF!5e0!3m2!1sen!2str!4v1234567890";
}

public class ContactFormViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string PreferredContactMethod { get; set; } = string.Empty;
    public DateTime? PreferredContactTime { get; set; }
}

