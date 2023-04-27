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



        [Required]
        [EmailAddress]
        public string email { get; set; }


        public DateTime creation_date { get; set; }

        public string ToQueryUpdate()
        {
            return $"UPDATE `empleado` SET `nombre`='{nombre}',`apellidos`='{apellidos}',`puesto`='{puesto}',`email`='{email}',`empresa`='{empresa}' WHERE `id`='{id}'";
        }

        public override string ToString()
        {
            return $"{nombre} {apellidos}";
        }

        internal string ToQuery()
        {
            return $"INSERT INTO `empleado`(`id`,`nombre`, `apellidos`, `empresa`, `puesto`, `email) VALUES ('{id}','{nombre}','{apellidos}','{empresa}','{puesto}','{email}')'";
        }

        public static implicit operator Employee(EmployeeModel v)
        {
            throw new NotImplementedException();
        }
    }
}
