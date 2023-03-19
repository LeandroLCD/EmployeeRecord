using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Employees
{
    public class Employee
    {
        public int id { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellidos { get; set; }


        [Required]
        public string puesto { get; set; } 

        [Required]
        public string empresa { get; set; } 

        public Rols rol { get; set; }


        [Required]
        [EmailAddress]
        public string email { get; set; }


        public DateTime creation_date { get; set; }
    }
}
