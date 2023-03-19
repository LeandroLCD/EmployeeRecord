using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Register;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeeRecord.Service.Implementation
{
    public class DataBaseService: IDataBaseService
    {
        private static IDataBaseConection _dataBaseConection;
        private MySqlConnection _connection;

        public DataBaseService()
        {
            try
            {
                _dataBaseConection = DependencyService.Get<IDataBaseConection>();

                _connection = _dataBaseConection.SqlConnection(Properties.Resources.db_conexion);
            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Employee Record", ex.Message, "ok");
            }


        }

        public Task<response> GetEmployeeList()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT `id`, `nombre`, `apellidos`, `empresa`, `puesto`  FROM `empleado` WHERE `empresa` != 'Z Motors'"; //ojito con esto
                    using (var reader =  cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<EmployeeModel>(reader);
                        return Task.FromResult( new response
                        {
                            Message = "Datos Cargados",
                            Objet = data,
                            Status = 200,
                            Success = true
                        });
                        
                    }
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(new response
                {
                    Message = $"Se produjo un error al tratar de obtener los empleados.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> GetTasksList()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT *  FROM `tasks`"; 
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<TasksModel>(reader);
                        return Task.FromResult(new response
                        {
                            Message = "Datos Cargados",
                            Objet = data,
                            Status = 200,
                            Success = true
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(new response
                {
                    Message = $"Se produjo un error al tratar de obtener los empleados.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> InsertRegisterIn(EmployeeRegister employee)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = employee.ToQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        return Task.FromResult(new response
                        {
                            Message = "Datos Registrados Exitosamente",
                            Status = 200,
                            Success = true
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(new response
                {
                    Message = $"Se produjo un error al tratar de obtener los empleados.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> InsertRegisterOut(EmployeeRegister employee)
        {
            throw new NotImplementedException();
        }
    }
}
