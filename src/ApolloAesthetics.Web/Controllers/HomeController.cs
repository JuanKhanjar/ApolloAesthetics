using Microsoft.AspNetCore.Mvc;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Application.DTOs;
using ApolloAesthetics.Web.Models;
using ApolloAesthetics.Domain.Entities;

namespace ApolloAesthetics.Web.Controllers;

public class HomeController : Controller
{
    private readonly IPatientService _patientService;
    private readonly IAppointmentService _appointmentService;
    private readonly IConsultationService _consultationService;

    public HomeController(
        IPatientService patientService,
        IAppointmentService appointmentService,
        IConsultationService consultationService)
    {
        _patientService = patientService;
        _appointmentService = appointmentService;
        _consultationService = consultationService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Doctors = new List<Doctor>
            {
                new Doctor { Id = 1, FirstName = "Dr. Mehmet", LastName = "Özkan", Specialization = "Dental Implants", ProfileImage = "/images/doctors/doctor1.jpg", YearsOfExperience = 15 },
                new Doctor { Id = 2, FirstName = "Dr. Ayşe", LastName = "Demir", Specialization = "Cosmetic Dentistry", ProfileImage = "/images/doctors/doctor2.jpg", YearsOfExperience = 12 },
                new Doctor { Id = 3, FirstName = "Dr. Ali", LastName = "Yılmaz", Specialization = "Orthodontics", ProfileImage = "/images/doctors/doctor3.jpg", YearsOfExperience = 10 }
            },
            Services = new List<MedicalService>
            {
                new MedicalService { Id = 1, ServiceName = "Dental Implants", Description = "High-quality dental implants", Price = 1500, Icon = "fas fa-tooth" },
                new MedicalService { Id = 2, ServiceName = "Teeth Whitening", Description = "Professional teeth whitening", Price = 300, Icon = "fas fa-smile" },
                new MedicalService { Id = 3, ServiceName = "Orthodontics", Description = "Braces and aligners", Price = 2000, Icon = "fas fa-teeth" },
                new MedicalService { Id = 4, ServiceName = "Hair Transplant", Description = "FUE hair transplantation", Price = 2500, Icon = "fas fa-user" }
            },
            Testimonials = new List<Testimonial>
            {
                new Testimonial { Id = 1, PatientName = "John Smith", Content = "Excellent service and professional staff!", Rating = 5, IsApproved = true },
                new Testimonial { Id = 2, PatientName = "Sarah Johnson", Content = "Amazing results, highly recommended!", Rating = 5, IsApproved = true },
                new Testimonial { Id = 3, PatientName = "Michael Brown", Content = "Professional and caring team.", Rating = 5, IsApproved = true }
            },
            GalleryImages = new List<GalleryImage>
            {
                new GalleryImage { Id = 1, Title = "Before & After", ImagePath = "/images/gallery/before-after-1.jpg", Category = "Dental" },
                new GalleryImage { Id = 2, Title = "Clinic Interior", ImagePath = "/images/gallery/clinic-1.jpg", Category = "Clinic" },
                new GalleryImage { Id = 3, Title = "Treatment Room", ImagePath = "/images/gallery/treatment-1.jpg", Category = "Clinic" }
            },
            Statistics = new Dictionary<string, int>
            {
                { "TotalPatients", 1250 },
                { "TotalAppointments", 3500 },
                { "TotalDoctors", 8 },
                { "TotalServices", 15 }
            }
        };

        return View(model);
    }

    public IActionResult About()
    {
        var model = new AboutViewModel
        {
            Title = "About Apollo Aesthetics",
            Description = "Apollo Aesthetics is a leading medical clinic specializing in dental care and aesthetic treatments.",
            Mission = "To provide world-class medical and dental care with the latest technology and techniques.",
            Vision = "To be the most trusted healthcare provider in the region.",
            Doctors = new List<Doctor>
            {
                new Doctor { Id = 1, FirstName = "Dr. Mehmet", LastName = "Özkan", Specialization = "Dental Implants", Biography = "15+ years of experience in dental implantology", YearsOfExperience = 15 },
                new Doctor { Id = 2, FirstName = "Dr. Ayşe", LastName = "Demir", Specialization = "Cosmetic Dentistry", Biography = "Expert in smile design and cosmetic procedures", YearsOfExperience = 12 }
            },
            Statistics = new Dictionary<string, int>
            {
                { "TotalPatients", 1250 },
                { "TotalDoctors", 8 },
                { "YearsOfExperience", 20 },
                { "SuccessRate", 98 }
            }
        };

        return View(model);
    }

    public IActionResult Services()
    {
        var model = new ServicesViewModel
        {
            Services = new List<MedicalService>
            {
                new MedicalService { Id = 1, ServiceName = "Dental Implants", Description = "High-quality dental implants with lifetime warranty", Price = 1500, Category = "Dental", Icon = "fas fa-tooth" },
                new MedicalService { Id = 2, ServiceName = "Teeth Whitening", Description = "Professional teeth whitening for a brighter smile", Price = 300, Category = "Dental", Icon = "fas fa-smile" },
                new MedicalService { Id = 3, ServiceName = "Orthodontics", Description = "Braces and clear aligners for perfect teeth alignment", Price = 2000, Category = "Dental", Icon = "fas fa-teeth" },
                new MedicalService { Id = 4, ServiceName = "Hair Transplant", Description = "FUE hair transplantation with natural results", Price = 2500, Category = "Aesthetic", Icon = "fas fa-user" },
                new MedicalService { Id = 5, ServiceName = "Botox Treatment", Description = "Anti-aging botox treatments", Price = 400, Category = "Aesthetic", Icon = "fas fa-syringe" }
            },
            Categories = new List<string> { "All", "Dental", "Aesthetic" }
        };

        return View(model);
    }

    public IActionResult Gallery()
    {
        var model = new GalleryViewModel
        {
            Images = new List<GalleryImage>
            {
                new GalleryImage { Id = 1, Title = "Before & After - Dental Implants", ImagePath = "/images/gallery/before-after-1.jpg", Category = "Before & After" },
                new GalleryImage { Id = 2, Title = "Clinic Reception", ImagePath = "/images/gallery/clinic-1.jpg", Category = "Clinic" },
                new GalleryImage { Id = 3, Title = "Treatment Room", ImagePath = "/images/gallery/treatment-1.jpg", Category = "Clinic" },
                new GalleryImage { Id = 4, Title = "Hair Transplant Results", ImagePath = "/images/gallery/hair-1.jpg", Category = "Before & After" },
                new GalleryImage { Id = 5, Title = "Dental Equipment", ImagePath = "/images/gallery/equipment-1.jpg", Category = "Equipment" }
            },
            Categories = new List<string> { "All", "Before & After", "Clinic", "Equipment" }
        };

        return View(model);
    }

    public IActionResult Contact()
    {
        var model = new ContactViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var consultationDto = new CreateConsultationRequestDto
        {
            FullName = model.ContactForm.Name,
            Email = model.ContactForm.Email,
            Phone = model.ContactForm.Phone,
            ServiceOfInterest = model.ContactForm.Subject,
            Message = model.ContactForm.Message,
            PreferredContactMethod = Enum.Parse<Domain.Enums.ContactMethod>(model.ContactForm.PreferredContactMethod),
            PreferredContactTime = model.ContactForm.PreferredContactTime
        };

        await _consultationService.CreateConsultationAsync(consultationDto);
        
        TempData["SuccessMessage"] = "Thank you for your message. We will contact you soon!";
        return RedirectToAction("Contact");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

