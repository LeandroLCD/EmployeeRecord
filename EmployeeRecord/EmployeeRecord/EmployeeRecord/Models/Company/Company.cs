using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Models.Company
{
    public class Company
    {
        public int id { get; set; }

        public string name { get; set; }

        public string ToQuery()
        {
            return $"INSERT INTO `empresa`(`id`, `name`) VALUES ('{id}','{name}')";
        }
    }
}
