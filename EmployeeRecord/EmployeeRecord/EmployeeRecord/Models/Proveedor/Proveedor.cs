using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Proveedor
{
    public class Proveedor
    {
        public int id { get; set; }

        public string provedor { get; set; }

        public string ToQuery()
        {
            return $"INSERT INTO `proveedor`(`id`, `provedor`) VALUES ('{id}','{provedor}')";
        }
    }
}
