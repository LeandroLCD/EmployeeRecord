using EmployeeRecord.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecord.Service.Interface
{
    public interface IEmailService
    {
        Task<response> SendEmail(string to, string subjet, string mensaje);

    }
}
