using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
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

        #endregion

        #region Methods

        private void InicializeProperty()
        {
            #region Validate Remember
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

            _autenticationService = DependencyService.Get<IAutenticationService>();

            #region SetCommand
            LoginCommand = new Command(LoginMethod);

            //SyncInCommand = new Command(async()=> App.GlobalNavigation.PopAsync(new SyncPage));
            //ForogotCommand = new Command(async () => App.GlobalNavigation.PopAsync(new ForogotPage));
            #endregion
        }

        private async void LoginMethod(object obj)
        {
            IsLoading = true;
            #region Validationes
            var valid = User.DataAnotationsValid();
            if(valid != null)
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
            
            if(Remember)
            {
                var user = JsonConvert.SerializeObject(User);
                Preferences.Set(nameof(User), user);
            }
            else
            {
                if(Preferences.ContainsKey(nameof(User)))
                    Preferences.Remove(nameof(User));
            }

            #endregion

            #region Login
            var resp = await _autenticationService.Login(User);
            if(resp.Success)
            {
                //navegar al Home
                // App.Current.MainPage = new NavigationPage(new HomePage);
                IsLoading = false;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", resp.Message, "Ok");
                IsLoading = false;
            }

            #endregion
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
