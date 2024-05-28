using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDoctorRepo
    {
        public bool RegisterDoc(DoctorModel doctorModel);
        public List<DoctorModel> FetchAllDocs();
        public DoctorModel FetchByDoctorId(int doctorId);
        public bool UpdateDoctor(DoctorModel doctorModel);

        public bool DeleteDoctorRecord(int doctorId);
    }
}
