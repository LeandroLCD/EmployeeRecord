using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Register;
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

        Task<response> GetEmployeeAllList();

        Task<response> GetTasksList();

        Task<response> InsertRegisterIn(EmployeeRegister employee);

        Task<response> UpdateRegisterOut(EmployeeRegister employee);

        Task<response> DeleteEmployee(Employee employee);

        Task<response> UpdateEmployee(Employee employee);

        Task<response> GetRegisterIn(string id);
    }
}
