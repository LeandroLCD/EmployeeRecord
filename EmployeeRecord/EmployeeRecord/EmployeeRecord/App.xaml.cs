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
        }

        private void RegisterServices()
        {
            DependencyService.Register<IAutenticationService, AutenticationService>();
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
