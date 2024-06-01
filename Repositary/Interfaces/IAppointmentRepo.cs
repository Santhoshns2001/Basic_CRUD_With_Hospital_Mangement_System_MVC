using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAppointmentRepo
    {
        public bool CreateAppointment(AppointmentModel appointmentmodel);

        public List<AppointmentModel> GetAllAppointments();

        public List<DoctorPatientModel> GetDoctorAndPatientProfiles();
    }
}
