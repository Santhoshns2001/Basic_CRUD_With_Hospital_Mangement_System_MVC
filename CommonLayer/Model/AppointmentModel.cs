using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }

        public int Doctor_Id { get; set; }

        public int PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Concerns { get; set; }



    }
}
