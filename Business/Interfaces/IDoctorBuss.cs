using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IDoctorBuss
    {
        public bool RegisterDoc(DoctorModel doctorModel);

        public List<DoctorModel> FetchAllDocs();
    }
}
