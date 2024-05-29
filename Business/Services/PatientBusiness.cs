using Business.Interfaces;
using CommonLayer.Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PatientBusiness : IPatientBuss
    {
        private readonly IPatientRepo patientRepo;

        public PatientBusiness(IPatientRepo patientRepo)
        {
            this.patientRepo = patientRepo;
        }

        public bool RegisterPatient(PatientModel patient)
        {
            return patientRepo.RegisterPatient(patient);
        }

        public List<PatientModel> GetAllPatients()
        {
            return patientRepo.GetAllPatients();
        }

        public PatientModel GetPatientById(int patientId)
        {
            return patientRepo.GetPatientById(patientId);
        }
        public bool UpdatePatient(PatientModel patient)
        {
            return patientRepo.UpdatePatient(patient);
        }
        public bool DeletePatientConfirmed(int patientId)
        {
            return patientRepo.DeletePatientConfirmed(patientId);
        }

        public PatientModel Login(LoginModel loginModel)
        {
            return patientRepo.Login(loginModel);
        }
        //public PatientModel LoginPatient(LoginModel model)
        //{
        //    return patientRepo.LoginPatient(model);
        //}
    }


}
