using Business.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentBuss appointmentBuss;

        public AppointmentController(IAppointmentBuss appointmentBuss)
        {
            this.appointmentBuss = appointmentBuss;  
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAppointment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAppointment(AppointmentModel appointmentModel)
        {
            bool result= appointmentBuss.CreateAppointment(appointmentModel);
            if (result) { return RedirectToAction("GetAllAppointments"); }
            return NotFound("unable to create appointment ");
        }

        [HttpGet]
        public IActionResult GetAllAppointments() 
        {
            List<AppointmentModel> appointments = appointmentBuss.GetAllAppointments();
            if (appointments != null) { return  View(appointments); }
            else return NotFound("unable to fetch appointments");
        }

        [HttpGet]
        public IActionResult GetDoctorAndPatientProfiles()
        {
           List<DoctorPatientModel> model= appointmentBuss.GetDoctorAndPatientProfiles();
            return View(model);
        }

    }
}
