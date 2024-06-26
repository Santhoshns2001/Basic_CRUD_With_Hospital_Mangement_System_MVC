﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class PatientModel
    {
        public int PatientId {  get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public long Contact { get; set; }

        public string Address { get; set; }

        public DateTime DOB { get; set; }

        public int Age { get; set; }

        public string  Gender { get; set; }

        public string PatientImage { get; set; }

        public bool IsTrash { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }




    }
}
