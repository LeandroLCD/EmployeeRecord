using EmployeeRecord.Service.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeRecord.Service.Implementation
{
    internal class DataBaseConection : IDataBaseConection
    {
        public MySqlConnection SqlConnection(string conection)
        {
            return new MySqlConnection(conection);
        }
    }
}
