using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using MySqlConnector;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.GTKSpecific;
//SOLID
namespace EmployeeRecord.Service.Implementation
{
    public class AutenticationService : IAutenticationService, IDisposable
    {
        private MySqlConnection _connection;

        public AutenticationService()
        {
            _connection = new MySqlConnection(Properties.Resources.db_conexion);
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
               App.Current.MainPage.DisplayAlert("Employee Record", ex.Message,"Ok" );
            }
            
            
        }

        

        public async Task<response> Login(UserAutentication user)
        {
            try
            {
                var cmd = new MySqlCommand(user.ToQuery());
                var rd = cmd.ExecuteReaderAsync();

                // TODO: revisar respuesta del server para almacenar los datos de user
                if (rd.Status != TaskStatus.Faulted)//si trae o no trae datos
                {
                    return new response
                    {
                        Success = true,
                        Status = 200,
                        Message = $"Bienvenido al sistema "
                        //retorar datos del user
                    };
                }

                return new response
                {
                    Success = false,
                    Status = rd.GetHashCode(),
                    Message = $"Correo o contraseña invalido"
                };
            }
            catch (Exception ex)
            {

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

                var cmd = new MySqlCommand(user.ToQuery());
                var rd = cmd.ExecuteReaderAsync();
                if(rd.IsCompleted)
                {
                    return  new response 
                    { 
                        Success = true,
                        Status = 200,
                        Message = $"Bienvenido al sistema de registro {user.Name}"
                    };
                }

                return new response 
                { 
                    Success= false,
                    Status = rd.GetHashCode(),
                    Message = $""
                };
                
            }
            catch(Exception ex) 
            {
                if (ex.Message.Contains("server off"))
                {
                    return new response()
                    {
                        Success = false,
                        Message = $"Se produjo una excepción al intentar registar un nuevo empleado.\nDetalles:{ex.Message}",
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
    }
}
