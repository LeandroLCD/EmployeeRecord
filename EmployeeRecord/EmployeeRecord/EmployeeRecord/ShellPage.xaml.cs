using EmployeeRecord.Service.Interface;
using EmployeeRecord.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeRecord
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellPage : Shell
    {
        private IAutenticationService _auteticationLogin;

        public ShellPage()
        {
            InitializeComponent();
            InicialiceRoutes();
            _auteticationLogin = DependencyService.Get<IAutenticationService>();
        }

        private void InicialiceRoutes()
        {
            //Aqui se registran todas las rutas de las paginas de app menos las de
            //login register ni forogot, solo las paginas de adentro de la app
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            _auteticationLogin.Logout();
        }
    }
}