using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmployeeRecord.Service.Implementation
{
    public class EmailService : IEmailService
    {
        public Task<response> SendEmail(string to, string subjet, string mensaje)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Properties.Resources.Server);

                mail.From = new MailAddress(Properties.Resources.Email);


                mail.To.Add(to);
                mail.Subject = subjet;
                mail.IsBodyHtml = true;
                mail.Body = mensaje;



                SmtpServer.Port = int.Parse(Properties.Resources.Port); //puerto del correo
                SmtpServer.Host = Properties.Resources.Server;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Properties.Resources.Email, Properties.Resources.Password);

                SmtpServer.Send(mail);

                return Task.FromResult(new response { Status = 200, Message = "Correo Enviado Exitosamente", Success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("locked"))
                {
                    return Task.FromResult(new response { Status = 400, Message = "La cuenta de correo ha sido bloqueada por Outlook contacte al administrador.", Success = false });
                }
                else
                {
                    return Task.FromResult(new response { Status = 400, Message = ex.Message, Success = false });
                }

            }
        }


    }
}
