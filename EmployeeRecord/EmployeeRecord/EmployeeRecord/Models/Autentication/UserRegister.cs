using EmployeeRecord.Models.Employees;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Autentication
{
    public class UserRegister : Employee
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "El correo no tiene formato de correo")]
        [Compare("Email", ErrorMessage ="Los Correos no coinciden")]
        public string VerifieEmail { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Las Contraseñas no coinciden")]
        public string VerifiePassword { get; set; }

        public DateTime CreationDate { get; set; }

        public string ToQuery()
        {
            return $"insert into empleado(nombre,apellidos,puesto,email,password,creation_date) values('{Name}','{LastName}','{Position}','{Email}','{Password}','{CreationDate}')";
        }
        //insert into empleado(nombre,apellidos,puesto,email,password) values('" + t_name.Text + "','" + t_lastname.Text + "','" + t_position.Text + "','" + t_email.Text + "','" + t_password.Text + "
    }
}
