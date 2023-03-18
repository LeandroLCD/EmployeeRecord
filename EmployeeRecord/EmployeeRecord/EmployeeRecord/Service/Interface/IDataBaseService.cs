using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecord.Service.Interface
{
    public interface IDataBaseService
    {
       Task<response> GetEmployeeList();

        Task<response> GetTasksList();

    }
}
