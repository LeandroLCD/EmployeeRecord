using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Register
{
    public class RegisterEventModel
    {
        
        public string nombreCompleto { get; set; }

        public string empresa { get; set; }

        public string motivo { get; set; }

        public string puesto { get; set; }

        public DateTime hora_entra { get; set; }

        public DateTime hora_sali { get; set; }

        public bool IsExcited { get; set; }
    }
}
