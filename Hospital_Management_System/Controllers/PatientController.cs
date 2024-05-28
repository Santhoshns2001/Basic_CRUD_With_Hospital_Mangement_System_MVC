using Business.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientBuss patientBuss;

        public PatientController(IPatientBuss patientBuss)
        {
            this.patientBuss = patientBuss;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RegisterPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPatient(PatientModel patientModel)
        {
            bool response = patientBuss.RegisterPatient(patientModel);

            if (response)
                return View("Index");
                return View(response);
            
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            List<PatientModel> patients = patientBuss.FetchAllPatients().ToList();
            if (patients.Any())
            {
                return View(patients);
            }
            else
            {
                return View("Index");
            }
        }

    }
}
