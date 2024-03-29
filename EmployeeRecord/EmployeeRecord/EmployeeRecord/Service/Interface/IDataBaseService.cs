﻿using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Company;
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
        Task<response> GetEmployeeZmotors();

        Task<response> GetProveedorIn();

        Task<response> GetTasksList();

        Task<response> InsertTask(TasksModel task);

        Task<response> GetCompanyList();

        Task<response> InsertCompany(Company company);

        Task<response> InsertRegisterIn(EmployeeRegister employee);

        Task<response> UpdateRegisterOut(EmployeeRegister employee);

        Task<response> DeleteEmployee(EmployeeModel employee);

        Task<response> UpdateEmployee(EmployeeModel employee);


        Task<response> GetRegisterIn(string id);

        Task<response> InsertRegisterProvIn(ProveedorModel proveedor);

        Task<response> UpdateProvedorOut(ProveedorModel getProveedor);

        Task<response> SearchRegister(DateTime fecha_ini, DateTime fecha_fin);
    }
}
