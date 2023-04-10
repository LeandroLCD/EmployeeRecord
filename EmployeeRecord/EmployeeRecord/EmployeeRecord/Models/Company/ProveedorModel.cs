using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Company
{
    public class ProveedorModel
    {
        private object idProveedor;

        public int id { get; set; }

        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string motivo { get; set; }

        public string empresa { get; set; }

        public DateTime hora_entra { get; set;}

        public DateTime hora_sali { get; set; }

        public bool IsExcited { get; set; }

        public string ToQuery()
        {
            if (IsExcited)
            {
                return $"UPDATE `regis_prov` SET `hora_sali`='{hora_sali.ToString("yyyy-MM-dd HH:mm:ss")}',`IsExcited`='1' WHERE id='{id}'";
                //`id`='{id}',`idEmpleado`='{idEmpleado}',`nombre`='{nombre}',`apellidos`='{apellidos}',`puesto`='{puesto}',`empresa`='{empresa}',
            }
            return $"insert into `regis_prov`(id,idProveedor,nombre,apellidos,empresa,motivo,hora_entra,hora_sali,IsExcited) values('{id}','{idProveedor}','{nombre}','{apellidos}','{empresa}','{motivo}','{hora_entra.ToString("yyyy-MM-dd HH:mm:ss")}','{hora_sali.ToString("yyyy-MM-dd HH:mm:ss")}','0')";

        }
    }
}
