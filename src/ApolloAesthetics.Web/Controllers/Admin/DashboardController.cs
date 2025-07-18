using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApolloAesthetics.Application.Interfaces;
using ApolloAesthetics.Domain.Constants;
using ApolloAesthetics.Web.Models.Admin;

namespace ApolloAesthetics.Web.Controllers.Admin;

[Authorize(Policy = Policies.StaffOrAbove)]
[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;
    private readonly IConsultationService _consultationService;
    private readonly IUserService _userService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(
        IAppointmentService appointmentService,
        IPatientService patientService,
        IConsultationService consultationService,
        IUserService userService,
        ILogger<DashboardController> logger)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _consultationService = consultationService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var model = new DashboardViewModel
            {
                TotalAppointments = await _appointmentService.GetTotalAppointmentsAsync(),
                TodayAppointments = await _appointmentService.GetTodayAppointmentsCountAsync(),
                TotalPatients = await _patientService.GetTotalPatientsAsync(),
                NewConsultations = await _consultationService.GetNewConsultationsCountAsync(),
                TotalUsers = await _userService.GetUsersCountAsync(),
                ActiveUsers = await _userService.GetActiveUsersCountAsync(),
                //RecentAppointments = await _appointmentService.GetRecentAppointmentsAsync(5),
                //RecentConsultations = await _consultationService.GetRecentConsultationsAsync(5)
            };

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading dashboard");
            return View(new DashboardViewModel());
        }
    }

    [Authorize(Policy = Policies.ManageAppointments)]
    public async Task<IActionResult> Appointments()
    {
        try
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading appointments");
            return View(new List<object>());
        }
    }

    [Authorize(Policy = Policies.ManagePatients)]
    public async Task<IActionResult> Patients()
    {
        try
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading patients");
            return View(new List<object>());
        }
    }

    [Authorize(Policy = Policies.ManageConsultations)]
    public async Task<IActionResult> Consultations()
    {
        try
        {
            var consultations = await _consultationService.GetAllConsultationsAsync();
            return View(consultations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading consultations");
            return View(new List<object>());
        }
    }

    [Authorize(Policy = Policies.ViewReports)]
    public IActionResult Reports()
    {
        return View();
    }

    [Authorize(Policy = Policies.ManageSystem)]
    public async Task<IActionResult> Users()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading users");
            return View(new List<object>());
        }
    }

    [Authorize(Policy = Policies.ManageSystem)]
    public IActionResult Settings()
    {
        return View();
    }
}

