using EmployeeRecord.Models.Employees;

namespace EmployeeRecord.Models.Autentication
{
    public class UserAutentication : Employee
    {
        public string ToQuery()
        {
            return string.Format("SELECT * FROM UserModel WHERE EmailField = '{0}' AND PasswordField = '{1}'", Email, Password);
        }
    }
}
