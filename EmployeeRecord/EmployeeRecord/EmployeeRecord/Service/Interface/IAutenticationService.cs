using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Utilities;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EmployeeRecord.Service.Interface
{
    public interface IAutenticationService
    {
        Task<response> Login(UserAutentication user);
        void Logout();

        Task<response> GetUserByEmail(string email);

        Task<response> Register(UserRegister user);
    }
}
