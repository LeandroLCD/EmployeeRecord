using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Service.Reports;
using EmployeeRecord.Utilities;
using EmployeeRecord.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private UserAutentication _user;
        private bool _remember;
        private IAutenticationService _autenticationService;
        private bool _isShowPrw;
        #endregion

        #region Contrunctor
        public LoginViewModel()
        {

            InicializeProperty();
        }



        #endregion

        #region Command
        public Command LoginCommand { get; set; }

        public Command ForogotCommand { get; set; }

        public Command SyncInCommand { get; set; }

        public Command ShowPwdCommand { get; set; }

        #endregion

        #region Property

        public UserAutentication User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool Remember
        {
            get => _remember;
            set => SetProperty(ref _remember, value);
        }
        public bool IsShowPrw
        {
            get => _isShowPrw;
            set => SetProperty(ref _isShowPrw, value);
        }

        #endregion

        #region Methods

        private void InicializeProperty()
        {
            #region Validate Remember
            IsShowPrw = true;
            var exist = Preferences.ContainsKey(nameof(User));
            Remember = exist;
            if (exist)
            {
                var user = JsonConvert.DeserializeObject<UserAutentication>(Preferences.Get(nameof(User), null));
                User = user;

            }
            else
            {
                User = new UserAutentication();
            }
            #endregion
            try
            {
                _autenticationService = DependencyService.Get<IAutenticationService>();
            }
            catch (Exception)
            {


            }


            #region SetCommand
            LoginCommand = new Command(LoginMethod);

            ShowPwdCommand = new Command(() => IsShowPrw = !IsShowPrw);

            SyncInCommand = new Command(async () =>
            {
                await App.GlobalNavigation.PushAsync(new RegisterPage(), true);
            });
            ForogotCommand = new Command(async () =>
            {
                await App.GlobalNavigation.PushAsync(new ForgotPasswordPage());
            });
            #endregion
        }

        private async void LoginMethod(object obj)
        {
            try
            {
                IsLoading = true;
                #region Validationes
                var valid = User.DataAnotationsValid();
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

                    //foreach (var it in valid)
                    //{
                    //    if (string.IsNullOrEmpty(errors))
                    //        errors = it;
                    //    else
                    //        errors += $"\n{it}";
                    //}
                    await App.Current.MainPage.DisplayAlert("Employee Record", errors, "Ok");
                    return;
                }

                if (Remember)
                {
                    var user = JsonConvert.SerializeObject(User);
                    Preferences.Set(nameof(User), user);
                }
                else
                {
                    if (Preferences.ContainsKey(nameof(User)))
                        Preferences.Remove(nameof(User));
                }

                #endregion

                #region Login
                var resp = await _autenticationService.Login(User);
                if (resp.Success)
                {
                    //navegar al Home
                    App.Current.MainPage = new MenuAdminPage();

                    IsLoading = false;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", resp.Message, "Ok");
                    IsLoading = false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", $"Se ha producido un error al intentar iniciar sesión.\nPor favor contacte al administrador. Error Code:{ex.GetHashCode()}", "Ok");
                return;
            }


        }

        private void RememberMothod(bool _isRemember)
        {
            if (_isRemember)
            {
                var user = JsonConvert.SerializeObject(User);
                Preferences.Set(nameof(User), user);
            }
            else
            {
                var exist = Preferences.ContainsKey(nameof(User));
                if (exist)
                    Preferences.Clear(nameof(User));
            }
        }

        #endregion
    }
}
