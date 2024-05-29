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
            List<PatientModel> patients = patientBuss.GetAllPatients().ToList();
            if (patients.Any())
            {
                return View(patients);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("GetByPatId/{id}")]
        public IActionResult GetByPatientId(int id)
        {
            if(id == 0)
                return BadRequest();

            PatientModel patient=patientBuss.GetPatientById(id);
            if(patient == null)
            {
                return NotFound("unable to find ");
            }
            else
            {
                return View(patient);
            }
        }


        [HttpGet]
        public IActionResult UpdatePatient(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PatientModel patient = patientBuss.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        public IActionResult UpdatePatient(int id, PatientModel patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound("patient id mismatch");
            }
            bool result = patientBuss.UpdatePatient(patient);
            if (result) return RedirectToAction("GetAllPatients");
            return NotFound("unable to update patient details");

        }


        [HttpGet]
        public IActionResult DeletePatient(int id)
        {
            if(id==0||id==null) return NotFound("id is"+id);
            PatientModel patient=patientBuss.GetPatientById(id);
            if (patient == null) { return NotFound("doctor not found"); }
            return View(patient);

        }

        [HttpPost,ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePatientConfirmed(int id)
        {
            if (id == 0) { return BadRequest(); }
         
           bool result= patientBuss.DeletePatientConfirmed(id);
            if (result) return RedirectToAction("GetAllPatients");
            return NotFound("unable to delete the patient ");
        }
    }
}
