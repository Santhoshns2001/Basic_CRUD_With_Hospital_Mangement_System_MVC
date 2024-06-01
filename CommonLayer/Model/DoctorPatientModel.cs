using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class DoctorPatientModel
    {
        public string DoctorName { get; set; }
        public string DoctorImage { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public long PatientContact { get; set; }
        public string PatientAddress { get; set; }
        public DateTime PatientDOB { get; set; }
        public int PatientAge { get; set; }

        public string PatientGender { get; set; }

        public string PatientImage { get; set; }

        public bool PatientIsTrash { get; set; }

        public DateTime PatientCreatedAt { get; set; }

        public DateTime PatientUpdatedAt { get; set; }


    }
}
