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
            bool v = doctorBuss.RegisterDoc(doctorModel);
            if (v)
            return View("Index");
            return View(doctorModel);

        }


        [HttpGet]     
        public IActionResult FetchAllDoctors()
        {
            List<DoctorModel> doctors = doctorBuss.FetchAllDocs().ToList();
            if (doctors.Any())
            {
                return View(doctors);
            }
            else
            {
                return View("Index");
            }
        }

    }
}
