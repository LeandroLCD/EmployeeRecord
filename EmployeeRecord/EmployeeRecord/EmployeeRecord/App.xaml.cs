using EmployeeRecord.Service.Implementation;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeRecord
{
    public partial class App : Application
    {
        private NavigationPage _loginPage;

        public static INavigation GlobalNavigation { get; set; }

        public App()
        {
            InitializeComponent();
            RegisterServices();

            _loginPage = new NavigationPage(new LoginPage());
            GlobalNavigation = _loginPage.Navigation;
            MainPage = _loginPage;

            //MainPage = new EntradasProvPage();
        }

        private async void RegisterServices()
        {
            try
            {
                DependencyService.Register<IDataBaseConection, DataBaseConection>();
                DependencyService.Register<IAutenticationService, AutenticationService>();
                DependencyService.Register<IDataBaseService, DataBaseService>();
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", $"Se ha producido un error al intentar iniciar sesión.\nPor favor contacte al administrador. Error Code:{ex.GetHashCode()}", "Ok");
            }
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
