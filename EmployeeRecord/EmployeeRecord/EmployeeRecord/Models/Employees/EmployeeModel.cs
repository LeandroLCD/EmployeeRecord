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
        public string empresa { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
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
    }
}
