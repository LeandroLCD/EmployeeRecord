using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xamarin.Essentials;

namespace EmployeeRecord.Models.Employees
{
    public class EmployeeModel
    {
        
        public int id { get; set; }

        [Required]
        public string empresa { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellidos { get; set; }

        [Required]
        public string puesto { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }


        
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
    }
}
