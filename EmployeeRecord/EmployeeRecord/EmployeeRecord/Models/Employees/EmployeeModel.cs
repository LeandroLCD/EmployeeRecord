using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace EmployeeRecord.Models.Employees
{
    public class EmployeeModel
    {
        
        public int idusuario { get; set; }

        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string puesto { get; set; }

        public override string ToString()
        {
            return $"{nombre} {apellidos}";
        }

    }
}
