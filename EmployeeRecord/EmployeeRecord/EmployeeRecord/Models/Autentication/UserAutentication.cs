using EmployeeRecord.Models.Employees;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Autentication
{
    public class UserAutentication 
    {
        public Rols rol { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(10)]
        public string Password { get; set; }
        public string ToQuery()
        {
            return string.Format("SELECT * FROM `login` WHERE email = '{0}' AND password = '{1}'", email, Password);
        }
    }
}
