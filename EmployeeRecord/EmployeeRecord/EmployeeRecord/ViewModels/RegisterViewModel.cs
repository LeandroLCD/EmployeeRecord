using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using System;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Fields
        private UserRegister _getUser;
        private IAutenticationService _autenticationService;
        private bool _isShowPrw;

        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            GetUserRegister = new UserRegister()
            {
                nombre = "prueba",
                apellidos = "prueba",
                puesto = "prueba",
                creation_date = DateTime.Now,
                email = "prueba@email.com",
                VerifieEmail = "prueba@email.com",
                password = "123456",
                VerifiePassword = "123456"

            }
                ;
            InicializeProperties();
        }


        #endregion

        #region Commands
        public Command RegisterCommand { get; set; }

        public Command LoginCommand { get; set; }

        public Command ShowPwdCommand { get; set; }

        #endregion

        #region Properties
        public UserRegister GetUserRegister
        {
            get 
            {
                if(_getUser == null)
                    _getUser = new UserRegister();
                return _getUser;
            }
            set => SetProperty(ref _getUser, value);
        }
        public bool IsShowPrw
        {
            get => _isShowPrw;
            set => SetProperty(ref _isShowPrw, value);
        }
        #endregion

        #region Methodos

        private void InicializeProperties()
        {
            _autenticationService = DependencyService.Get<IAutenticationService>();
            RegisterCommand = new Command(RegisterMethod);
        }

        private async void RegisterMethod(object obj)
        {
            #region Validaciones
            IsLoading = true;
            var valid = GetUserRegister.DataAnotationsValid();
            if (valid != null)
            {
                string errors = string.Empty;

                valid.ForEach((it) =>
                {
                    if (string.IsNullOrEmpty(errors))
                        errors = it;
                    else
                        errors += $"\n{it}";
                });


                await App.Current.MainPage.DisplayAlert("Employee Record", errors, "Ok");
                IsLoading = false;
                return;
            }
            #endregion

          var response = await _autenticationService.Register(GetUserRegister);
            if(response.Success)
            { 
                GetUserRegister = new UserRegister();
                await App.Current.MainPage.DisplayAlert("Employee Record", "Usuario registrado, ya puedes acceder a nuestros sistema.", "Ok");
                await App.GlobalNavigation.PopToRootAsync();
            }
            else
            {

                await App.Current.MainPage.DisplayAlert("Employee Record", response.Message, "Ok");
            }
            IsLoading = false;
        }
        #endregion
    }
}
