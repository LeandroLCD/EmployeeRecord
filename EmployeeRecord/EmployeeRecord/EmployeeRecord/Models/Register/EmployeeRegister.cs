using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace EmployeeRecord.Models.Register
{
    public class EmployeeRegister
    {
        public EmployeeRegister()
        {
            IsExist = false;
        }
        public int id { get; set; }

        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string puesto { get; set; }

        public string empresa { get; set; }

        public string motivo { get; set; }

        public DateTime hora_entra { get; set; }

        public DateTime hora_sali { get; set; }

        public bool IsExist { get; set; }

        public string ToQuery()
        {
            if(IsExist)
            {
                return $"insert into `regis_bita`(id,nombre,puesto,empresa,motivo,hora_entra,hora_sali,IsExcited) values('{id}','{nombre}','{puesto}','{empresa}','{motivo}','{hora_entra.ToString("yyyy-MM-dd HH:mm:ss")}','{hora_sali.ToString("yyyy-MM-dd HH:mm:ss")},'1')";

            }
            return $"insert into `regis_bita`(id,nombre,puesto,empresa,motivo,hora_entra,hora_sali,IsExcited) values('{id}','{nombre}','{puesto}','{empresa}','{motivo}','{hora_entra.ToString("yyyy-MM-dd HH:mm:ss")}','{hora_sali.ToString("yyyy-MM-dd HH:mm:ss")}','0')";

        }
    }
}
