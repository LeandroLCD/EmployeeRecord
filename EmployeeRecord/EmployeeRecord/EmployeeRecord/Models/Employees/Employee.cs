using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Employees
{
    public class Employee
    {
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}
