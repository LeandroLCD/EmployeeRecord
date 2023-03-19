using EmployeeRecord.Models.Employees;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Autentication
{
    public class UserRegister : Employee
    {

        [Required]
        public string password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El correo no tiene formato de correo")]
        [Compare("email", ErrorMessage ="Los Correos no coinciden")]
        public string VerifieEmail { get; set; }


        [Required]
        [Compare("password", ErrorMessage = "Las Contraseñas no coinciden")]
        public string VerifiePassword { get; set; }



        public string ToQueryRegister()
        {
            return $"insert into `empleado`(nombre,apellidos,puesto,email,organizacion,creation_date) values('{nombre}','{apellidos}','{puesto}','{email}','{organizacion}','{creation_date.ToString("yyyy-MM-dd HH:mm:ss")}')";
        }
        public string ToQueryRegisterLogin()
        {
            return $"insert into `login`(email,password,rol) values('{email}','{password}','{rol}')";
        }
        //insert into empleado(nombre,apellidos,puesto,email,password) values('" + t_name.Text + "','" + t_lastname.Text + "','" + t_position.Text + "','" + t_email.Text + "','" + t_password.Text + "
    }
}
