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

        public List<PatientModel> FetchAllPatients()
        {
            return patientRepo.GetAllPatient();
        }
    }
}
