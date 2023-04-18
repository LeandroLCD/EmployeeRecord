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
                            $"<p>Se solicita recuperar contraseña desde el dispositivo, </p>" +
                            $"<p>Tu contrseña de acceso es: {user.Password}</p>" +
                            $"<p>Att el equipo de Employe Record</p>");

                        if(sendemail.Success)
                        {
                            //notificar al usuario correo enviado
                            //limpiar el entry
                            //navegar al login
                        }
                        else
                        {
                            //notificar error al usuario
                        }
                    }
                    else
                    {
                        //notificar error al usuario
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
