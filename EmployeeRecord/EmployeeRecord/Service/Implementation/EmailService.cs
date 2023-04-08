using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
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
                SmtpClient SmtpServer = new SmtpClient("smtpServer");

                mail.From = new MailAddress("emailFrom");

                
                mail.To.Add(to);
                mail.Subject = subjet;
                mail.IsBodyHtml = true;
                mail.Body = mensaje;
                


                SmtpServer.Port = 20; //puerto del correo
                SmtpServer.Host = "smtpServer";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("emailFrom", "Password");

                SmtpServer.Send(mail);

                return Task.FromResult(new response { Status = 200, Message = "Correo Enviado Exitosamente", Success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Authentication unsuccessful, account locked"))
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
