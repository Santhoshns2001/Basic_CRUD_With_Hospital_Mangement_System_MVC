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

        public List<PatientModel> GetAllPatients();

        public PatientModel GetPatientById(int patientId);

        public bool UpdatePatient(PatientModel patient);

        public bool DeletePatientConfirmed(int patientId);

        public PatientModel Login(LoginModel loginModel);
        //public PatientModel LoginPatient(LoginModel model);

    }
}
