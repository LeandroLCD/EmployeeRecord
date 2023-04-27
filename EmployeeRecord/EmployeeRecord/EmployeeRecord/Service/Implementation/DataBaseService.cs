using EmployeeRecord.Models.Company;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Register;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeeRecord.Service.Implementation
{

    public class DataBaseService : IDataBaseService
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

        public Task<response> GetProveedorIn()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM `regis_prov` WHERE IsExcited = 0";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<ProveedorModel>(reader);
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
                    Message = $"Se produjo un error al tratar de obtener la lista empleados.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }


        public Task<response> GetEmployeeZmotors()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM `empleado`";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<EmployeeModel>(reader);
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
                    Message = $"Se produjo un error al tratar de obtener la lista empleados.\nDetalles:{ex.Message}",
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

        public Task<response> DeleteEmployee(EmployeeModel employee)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM `empleado` WHERE id = {employee.id} && email = '{employee.email}'";
                    using (var reader = cmd.ExecuteReader())
                    {

                        return Task.FromResult(new response
                        {
                            Message = "Employee Eliminado Exitosamente",
                            Objet = employee,
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
                    Message = $"Se produjo un error al tratar de eliminar el empleado.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> UpdateRegisterOut(EmployeeRegister employee)
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
                            Message = "Employee Actualizado Exitosamente",
                            Objet = employee,
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
                    Message = $"Se produjo un error al tratar de eliminar el empleado.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = employee.ToQueryUpdate();
                    using (var reader = cmd.ExecuteReader())
                    {

                        return Task.FromResult(new response
                        {
                            Message = "Employee Actualizado Exitosamente",
                            Objet = employee,
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
                    Message = $"Se produjo un error al tratar de eliminar el empleado.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> GetRegisterIn(string id)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT *  FROM `regis_bita`  WHERE idEmpleado = '{id}' & IsExcited = '0'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<EmployeeRegister>(reader);
                        if (data.FirstOrDefault() != null)
                        {
                            return Task.FromResult(new response
                            {
                                Message = "Datos Cargados",
                                Objet = data.FirstOrDefault(),
                                Status = 200,
                                Success = true
                            });
                        }
                        return Task.FromResult(new response
                        {
                            Message = $"No se encontraron datos para el id del empleado {id}",
                            Objet = id,
                            Status = 201,
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

        public Task<response> GetCompanyList()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT *  FROM `empresa`";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<Company>(reader);
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
                    Message = $"Se produjo un error al tratar de obtener las empresas.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> InsertTask(TasksModel task)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = task.ToQuery();
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
                    Message = $"Se produjo un error al tratar de insertar nueva tarea.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> InsertCompany(Company company)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = company.ToQuery();
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
                    Message = $"Se produjo un error al tratar de insertar la empresa.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> InsertRegisterProvIn(ProveedorModel proveedor)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = proveedor.ToQuery();
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
                    Message = $"Se produjo un error al tratar de registrar el proveedor.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> UpdateProvedorOut(ProveedorModel getProveedor)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = getProveedor.ToQuery();
                    using (var reader = cmd.ExecuteReader())
                    {

                        return Task.FromResult(new response
                        {
                            Message = "Actualizado Exitosamente",
                            Objet = getProveedor,
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
                    Message = $"Se produjo un error al tratar de actualizar el proveedor.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> SearchRegister(DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT `nombre`, `apellidos`, `puesto`, `empresa`, `motivo`, `hora_entra`, `hora_sali`, `IsExcited`
                                        FROM (
                                            SELECT `nombre`, `apellidos`, `puesto`, `empresa`, `motivo`, `hora_entra`, `hora_sali`, `IsExcited` FROM `regis_prov`
                                            UNION ALL
                                            SELECT `nombre`, `apellidos`, `puesto`, `empresa`, `motivo`, `hora_entra`, `hora_sali`, `IsExcited` FROM `regis_bita`
                                        ) as combined_data
                                        WHERE `hora_entra` BETWEEN '{fecha_ini.Date.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{fecha_fin.Date.ToString("yyyy-MM-dd")} 23:59:59';

                                        ";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<RegisterEventModel>(reader);
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
                    Message = $"Se produjo un error al tratar de obtener los registros.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> CreateEmployee(Employee employee)
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
                    Message = $"Se produjo un error al agregar este nuevo usuario.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }

        public Task<response> CreateEmployee(EmployeeModel employee)
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
                            Message = "Usuario Registrado Exitosamente",
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
                    Message = $"Se produjo un error al intentar agregar este nuevo usuario.\nDetalles:{ex.Message}",
                    Objet = ex,
                    Status = ex.GetHashCode(),
                    Success = false
                });
            }
        }
    }

}
