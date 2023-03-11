using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Employees
{
    public class Employee
    {
        public int idusuario { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellidos { get; set; }


        [Required]
        public string puesto { get; set; }


        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        public DateTime creation_date { get; set; }
    }
}
