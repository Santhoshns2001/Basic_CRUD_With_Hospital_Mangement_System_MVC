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
    public class AppointmentBusiness:IAppointmentBuss
    {
        private readonly IAppointmentRepo appointmentRepo;
        public AppointmentBusiness(IAppointmentRepo appointmentRepo)
        {
            this.appointmentRepo = appointmentRepo;   
        }
        public bool CreateAppointment(AppointmentModel appointmentmodel)
        {
            return appointmentRepo.CreateAppointment(appointmentmodel);
        }

        public List<AppointmentModel> GetAllAppointments()
        {
            return appointmentRepo.GetAllAppointments();
        }
    }
}
