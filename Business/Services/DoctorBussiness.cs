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
    public class DoctorBussiness : IDoctorBuss
    {
        private readonly IDoctorRepo doctorRepo;

        public DoctorBussiness(IDoctorRepo doctorRepo)
        {
            this.doctorRepo = doctorRepo;
        }



       public bool RegisterDoc(DoctorModel doctorModel)
        {
           return doctorRepo.RegisterDoc(doctorModel);
        }

        public List<DoctorModel> FetchAllDocs()
        {
            return doctorRepo.FetchAllDocs();
        }
    }
}
