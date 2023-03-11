using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.GTKSpecific;
//SOLID
namespace EmployeeRecord.Service.Implementation
{
    public class AutenticationService : IAutenticationService, IDisposable
    {
        private static MySqlConnection _connection;

        public AutenticationService()
        {
            _connection = new MySqlConnection(Properties.Resources.db_conexion);
             _connection.Open();
            
        }

        

        public async Task<response> Login(UserAutentication user)
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                using (var cmd = _connection.CreateCommand())
                {

                    cmd.CommandText = user.ToQuery(); ; // "SELECT * FROM `empleado`";
                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        var data = DataReaderMapToList<Employee>(reader);
                        if (data.Count > 0)
                        {
                            return new response
                            {
                                Success = true,
                                Status = 200,
                                Message = $"Bienvenido al sistema ",
                                Objet = data.FirstOrDefault(u => u.email == user.Email)

                            };
                        }
                        else
                        {
                            return new response()
                            {
                                Success = false,
                                Message = $"Usuario o contraseña incorrecto.",
                                Status = 300


                            };
                        }
                        
                    }
                }

                
                
            }
            catch (Exception ex)
            {
                _connection.Close();
                return new response()
                {
                    Success = false,
                    Message = $"Se produjo una excepción al intentar ingresar al sistema \nDetalles:{ex.Message}",
                    Status = ex.GetHashCode()


                };
            }
        }

        public void Logout()
        {
            //Clear user.
            //App.Current.MainPage = new NavigationPage(new Login);
        }

        public async Task<response> Register(UserRegister user)
        {
            try 
            {
                
                if(_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                var cmd = _connection.CreateCommand(); 
                cmd.CommandText = user.ToQuery(); 
                var rd = cmd.ExecuteReader();

                    return  new response 
                    { 
                        Success = true,
                        Status = 200,
                        Message = $"Bienvenido al sistema de registro {user.nombre}"
                    };
            }
            catch(Exception ex) 
            {
                if (ex.Message.Contains("Connection reset by peer"))
                {
                    return new response()
                    {
                        Success = false,
                        Message = $"Se produjo una excepción al intentar registar un nuevo usuario, es probable que la base de datos este fuera de linea.\nDetalles:{ex.Message}",
                        Status = ex.GetHashCode()
                    }; 
                }
                else if(ex.Message.Contains("Duplicate entry"))
                {
                    return new response()
                    {
                        Success = false,
                        Message = $"El correo ingresado ya se encuentra registrado, por favor recupere su contraseña.",
                        Status = ex.GetHashCode()
                    };

                }

                else
                {
                    return new response()
                    {
                        Success = false,
                        Message = $"Se produjo una excepción al intentar registar un nuevo empleado.\nDetalles:{ex.Message}",
                        Status = ex.GetHashCode() 


                    };
                }
                
            }
        }
        //Que el server este off
        //que la tabla no exista.
        //Que el usuario ya exista
        public void Dispose()
        {
            _connection.Close();
        }


        public static List<T> DataReaderMapToList<T>(IDataReader reader)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (reader.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                    {
                        try 
                        {
                        prop.SetValue(obj, reader[prop.Name], null);
                        } 
                        catch 
                        {
                            continue;
                        }

                        
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
