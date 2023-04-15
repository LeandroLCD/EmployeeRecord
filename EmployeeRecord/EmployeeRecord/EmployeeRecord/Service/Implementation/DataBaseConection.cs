using EmployeeRecord.Service.Interface;
using MySqlConnector;

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
