using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using EmployeeRecord.Views;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
//SOLID
namespace EmployeeRecord.Service.Implementation
{
    public class AutenticationService : IAutenticationService, IDisposable
    {
        private static IDataBaseConection _dataBaseConection;
        private MySqlConnection _connection;

        public AutenticationService()
        {
            try
            {
                _dataBaseConection = DependencyService.Get<IDataBaseConection>();

                _connection = _dataBaseConection.SqlConnection(Properties.Resources.db_conexion);
            }
            catch (Exception ex)
            {
                var error = ex.Message;

                App.Current.MainPage.DisplayAlert("Employee Record", ex.Message, "ok");
            }
            

        }
        public Task<response> Login(UserAutentication user)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = user.ToQuery();  // "SELECT * FROM `empleado`";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var data = DataReader.MapToList<UserAutentication>(reader);
                        if (data.Count > 0)
                        {
                            return Task.FromResult(new response
                            {
                                Success = true,
                                Status = 200,
                                Message = $"Bienvenido al sistema",
                                Objet = data.FirstOrDefault(u => u.email == user.email)
                            });
                        }
                        else
                        {
                            return Task.FromResult(new response()
                            {
                                Success = false,
                                Message = $"Usuario o contraseña incorrecto.",
                                Status = 300
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _connection.Close();
                return Task.FromResult(new response()
                {
                    Success = false,
                    Message = $"Se produjo una excepción al intentar ingresar al sistema \nDetalles:{ex.Message}",
                    Status = ex.GetHashCode()
                });
            }
        }
        public void Logout()
        {
            //Clear user.
            var login = new NavigationPage(new LoginPage());
            App.GlobalNavigation = login.Navigation;
            App.Current.MainPage = login;
        }
        public Task<response> Register(UserRegister user)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                //registra el login
                using (var cmd = _connection.CreateCommand())
                {
                    using(var transaction = _connection.BeginTransaction())
                    {
                        cmd.Transaction = transaction;
                        var query = $"{user.ToQueryRegister()};\n{user.ToQueryRegisterLogin()};";

                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }

                }
                   

                return Task.FromResult(new response
                {
                    Success = true,
                    Status = 200,
                    Message = $"Bienvenido al sistema de registro {user.nombre}"
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Connection reset by peer"))
                {
                    return Task.FromResult(new response()
                    {
                        Success = false,
                        Message = $"Se produjo una excepción al intentar registar un nuevo usuario, es probable que la base de datos este fuera de linea.\nDetalles:{ex.Message}",
                        Status = ex.GetHashCode()
                    });
                }
                else if (ex.Message.Contains("Duplicate entry"))
                {
                    return Task.FromResult(new response()
                    {
                        Success = false,
                        Message = $"El correo ingresado ya se encuentra registrado, por favor recupere su contraseña.",
                        Status = ex.GetHashCode()
                    });
                }
                else
                {
                    return Task.FromResult(new response()
                    {
                        Success = false,
                        Message = $"Se produjo una excepción al intentar registar un nuevo empleado.\nDetalles:{ex.Message}",
                        Status = ex.GetHashCode()
                    });
                }
            }
        }
        public void Dispose()
        {
            _connection.Close();
        }
        
    }
}
