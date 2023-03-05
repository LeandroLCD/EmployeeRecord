using EmployeeRecord.Models.Employees;
using System;

namespace EmployeeRecord.Models.Autentication
{
    public class UserRegister : Employee
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string VerifieEmail { get; set; }

        public string VerifiePassword { get; set; }

        public DateTime CreationDate { get; set; }

        public string ToQuery()
        {
            return string.Format("insert into empleado(nombre,apellidos,puesto,email,password values({'0'},{'1'},{'2'},{'3'},{'4'})", Name, LastName, Position, Email, Password);
        }
        //insert into empleado(nombre,apellidos,puesto,email,password) values('" + t_name.Text + "','" + t_lastname.Text + "','" + t_position.Text + "','" + t_email.Text + "','" + t_password.Text + "
    }
}
