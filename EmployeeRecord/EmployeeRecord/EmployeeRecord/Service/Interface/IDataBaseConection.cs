using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecord.Service.Interface
{
    public interface IDataBaseConection
    {
       MySqlConnection SqlConnection(string conection);
    }
}
