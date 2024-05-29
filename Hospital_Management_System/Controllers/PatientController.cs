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
            bool result = patientBuss.RegisterPatient(patientModel);
            if (result) return RedirectToAction("GetAllPatients");
            return NotFound("Failed to insert patient");

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
        //[Route("GetByPatId/{id}")]
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

        [HttpGet]
        public IActionResult LoginPatient()
        {
            return View();
        }


        [HttpPost]
        //[Route("login/{patientid}/{patientName}")]
        public IActionResult LoginPatient(LoginModel patient)
        {
            if (patient == null) return NotFound("module cannot be null");
            if (patient.UserId==0 || patient.UserName == null) { return NotFound("Id or name is null "); }
            PatientModel result = patientBuss.Login(patient);
            if (result == null) return NotFound("unable to login patient");
            return RedirectToAction("GetByPatientId", new { Id=patient.UserId });
            //return RedirectToAction("GetEmpById", new { Id = result.EmployeeId});
        }
    }
}
