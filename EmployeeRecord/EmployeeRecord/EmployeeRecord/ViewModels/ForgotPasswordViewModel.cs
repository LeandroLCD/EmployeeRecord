using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels
{
    public class ForgotPasswordViewModel: BaseViewModel
    {
        #region Fields
        private IAutenticationService _serviceAuth;
        private IEmailService _serviceEmail;

        #endregion

        #region Ctor
        public ForgotPasswordViewModel()
        {
            _serviceAuth = DependencyService.Get<IAutenticationService>();

            _serviceEmail = DependencyService.Get<IEmailService>();

            SendEmailCommand = new Command<string>(async (email) =>
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(email)) // validar formato de correo
                {
                    var response = await _serviceAuth.GetUserByEmail(email);
                    if (response.Success)
                    {
                        var user = (UserAutentication)response.Objet;
                      var sendemail = await  _serviceEmail.SendEmail(email, "Recuperar contraseña Employee Record", 
                            $"<p>Se solicito recuperar contraseña desde este dispositivo, </p>" +
                            $"<p>Tu contrseña de acceso es: {user.Password}</p>" +
                            $"<p>Att Sistema de monitoreo  y registro para el site</p>");

                        if(sendemail.Success)
                        {
                            //notificar al usuario correo enviado
                            await App.Current.MainPage.DisplayAlert("Employee Record", "Contraseña enviada con exito, revisa tu correo", "Ok");
                            //limpiar el entry
                            
                            //navegar al login
                        }
                        else
                        {
                            //notificar error al usuario
                            await App.Current.MainPage.DisplayAlert("Employee Record", "Error Al Enviar Contraseña, Verifica el correo que introduciste", "Ok");
                        }
                    }
                    else
                    {
                        //notificar error al usuario
                        await App.Current.MainPage.DisplayAlert("Employee Record", "Error Al Enviar Contraseña, Verifica el correo que introduciste", "Ok");
                    }
                }
                IsLoading = false;
            });
        }
        #endregion

        #region Command

        public Command<string> SendEmailCommand { get; set; }

        #endregion
    }
}
