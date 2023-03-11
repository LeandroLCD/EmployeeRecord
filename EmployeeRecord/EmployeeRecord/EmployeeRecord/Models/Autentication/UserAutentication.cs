using EmployeeRecord.Models.Employees;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Autentication
{
    public class UserAutentication 
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(10)]
        public string Password { get; set; }
        public string ToQuery()
        {
            return string.Format("SELECT * FROM `empleado` WHERE email = '{0}' AND password = '{1}'", Email, Password);
        }
    }
}
