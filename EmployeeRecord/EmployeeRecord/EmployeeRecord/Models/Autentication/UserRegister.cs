using EmployeeRecord.Models.Employees;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeRecord.Models.Autentication
{
    public class UserRegister : Employee
    {
        
       
        [Required]
        [EmailAddress(ErrorMessage = "El correo no tiene formato de correo")]
        [Compare("email", ErrorMessage ="Los Correos no coinciden")]
        public string VerifieEmail { get; set; }


        [Required]
        [Compare("password", ErrorMessage = "Las Contraseñas no coinciden")]
        public string VerifiePassword { get; set; }



        public string ToQuery()
        {
            return $"insert into `empleado`(nombre,apellidos,puesto,email,password,creation_date) values('{nombre}','{apellidos}','{puesto}','{email}','{password}','{creation_date.ToString("yyyy-MM-dd HH:mm:ss")}')";
        }
        //insert into empleado(nombre,apellidos,puesto,email,password) values('" + t_name.Text + "','" + t_lastname.Text + "','" + t_position.Text + "','" + t_email.Text + "','" + t_password.Text + "
    }
}
