using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Register
{
    public class ProveedorModel
    {
        public int id { get; set; }

        public string nombreCompleto { get; set; }

        public string motivo { get; set; }

        public string empresa { get; set; }

        public string puesto { get; set; }

        public DateTime hora_entra { get; set;}

        public DateTime hora_sali { get; set; }

        public bool IsExcited { get; set; }

        public string ToQuery()
        {
            if (IsExcited)
            {
                return $"UPDATE `regis_prov` SET `hora_sali`='{hora_sali.ToString("yyyy-MM-dd HH:mm:ss")}',`IsExcited`='1' WHERE id='{id}'";
                 }
            return $"insert into `regis_prov`(id,nombreCompleto,puesto,empresa,motivo,hora_entra,hora_sali,IsExcited) values('{id}','{nombreCompleto}','{puesto}','{empresa}','{motivo}','{hora_entra.ToString("yyyy-MM-dd HH:mm:ss")}','{hora_sali.ToString("yyyy-MM-dd HH:mm:ss")}','0')";

        }

        public override string ToString()
        {
            return $"{nombreCompleto}";
        }
    }
}
