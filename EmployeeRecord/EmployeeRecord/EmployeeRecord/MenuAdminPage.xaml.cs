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
    public partial class MenuAdminPage : Shell
    {
        private IAutenticationService _auteticationLogin;
        public MenuAdminPage()
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

            #region Crews TI
            Routing.RegisterRoute(nameof(EntradasTiPage), typeof(EntradasTiPage));
            Routing.RegisterRoute(nameof(SalidasTiPage), typeof(SalidasTiPage));

            #endregion

            #region Crews Prov
            Routing.RegisterRoute(nameof(EntradasProvPage), typeof(EntradasProvPage));
            Routing.RegisterRoute(nameof(SalidasProvPage), typeof(SalidasProvPage));

            #endregion

            #region Crews Usuarios
            Routing.RegisterRoute(nameof(UserListPage), typeof(UserListPage));
            Routing.RegisterRoute(nameof(UserDetailPage), typeof(UserDetailPage));
            #endregion

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            _auteticationLogin.Logout();
        }

        protected override bool OnBackButtonPressed()
        {

            return OnBackButton();
        }
        public static bool OnBackButton()
        {
            var uriOriginal = Shell.Current.CurrentState.Location.OriginalString;
            var array = uriOriginal.Split('/');
            if (array.Length > 3)
            {
                var curren = array[array.Length - 1];
                var BackUry = uriOriginal.Remove(uriOriginal.Length - curren.Length, curren.Length);
                Shell.Current.GoToAsync(BackUry);
                return true;
            }
            return false;
        }
    }
}