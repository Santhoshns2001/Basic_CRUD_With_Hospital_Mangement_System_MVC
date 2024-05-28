using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPatientRepo
    {
        public bool RegisterPatient(PatientModel patient);

        public List<PatientModel> GetAllPatient();

        public PatientModel GetPatientById(int patientId);
    }
}
