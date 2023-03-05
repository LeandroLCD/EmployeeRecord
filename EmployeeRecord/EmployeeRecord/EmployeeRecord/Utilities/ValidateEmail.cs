using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using System.Text.RegularExpressions;

namespace EmployeeRecord.Utilities
{
    public static class ValidateEmail
    {
        
        public static bool IsValidateEmail(string email)
        {
            string  _patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, _patron);
        }

        public static bool VerifieString(string x, string y)
        {
            return x == y;
        }
    }
}
