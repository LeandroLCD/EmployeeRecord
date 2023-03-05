using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Utilities;
using System.Threading.Tasks;

namespace EmployeeRecord.Service.Interface
{
    public interface IAutenticationService
    {
        Task<response> Login(UserAutentication user);

        void Logout();

        Task<response> Register(UserRegister user);
    }
}
