using Business.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorBuss doctorBuss;

        public DoctorController(IDoctorBuss doctorBuss)
        {
            this.doctorBuss = doctorBuss;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterDoc()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterDoc(DoctorModel doctorModel )
        {
            bool result = doctorBuss.RegisterDoc(doctorModel);
            if (result)  return RedirectToAction("FetchAllDoctors"); 
             return NotFound("Failed to insert doctor"); 
            

        }


        [HttpGet]     
        public IActionResult FetchAllDoctors()
        {
            List<DoctorModel> doctors = doctorBuss.FetchAllDocs().ToList();
            if (doctors!=null)
            {
                return View(doctors);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("GetById/{DoctorId}")]
        public IActionResult FetchByDoctorId(int doctorId)
        {
            DoctorModel doctor = doctorBuss.FetchByDoctorId(doctorId);

            if (doctor != null)
            {
                return View(doctor);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult UpdateDoctor(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DoctorModel doctor = doctorBuss.FetchByDoctorId(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
         
        [HttpPost]
        public IActionResult UpdateDoctor(int id, DoctorModel doctor)
        {
            if (id != doctor.Doctor_Id)
                return NotFound("Missmatch id");
            else
            {
                bool response = doctorBuss.UpdateDoctor(doctor);
                return RedirectToAction("FetchAllDoctors");

            }
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null || id==0)
            {
                return NotFound("id is "+id);
            }
            DoctorModel doctor = doctorBuss.FetchByDoctorId(id);
            if(doctor == null)
            {
                return NotFound("doctor not found");
            }
            return View(doctor);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 0)
                return BadRequest();
           bool result= doctorBuss.DeleteDoctorRecord(id);
            if(result) return RedirectToAction("FetchAllDoctors");
            return NotFound("Failed to delete");
        }


    }
}
