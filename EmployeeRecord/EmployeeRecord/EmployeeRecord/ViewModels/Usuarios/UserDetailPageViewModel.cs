using EmployeeRecord.Models.Employees;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Utilities;
using System;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.Usuarios
{
    public class UserDetailPageViewModel : BaseViewModel
    {
        #region Fields
        private EmployeeModel _employeeSelected;
        public static EmployeeModel GetEmployee { get; set; }

        private IDataBaseService _dataBaseService;
        private bool _isCreate;

        #endregion

        #region Constructor
        public UserDetailPageViewModel()
        {
            if (GetEmployee != null)
            {
                EmployeeSelected = GetEmployee;
                GetEmployee = null;
            }
            else
            {
                App.Current.MainPage.Title = "Crear Empleado";
                IsCreate = true;
                EmployeeSelected = new EmployeeModel();
            }
            InicializeProperties();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Muestra la lista de los empleados en la vista (Picker)
        /// </summary> 

        public EmployeeModel EmployeeSelected
        {
            get => _employeeSelected;
            set => SetProperty(ref _employeeSelected, value);
        }

        public bool IsCreate
        {
            get => _isCreate;
            set => SetProperty(ref _isCreate, value);
        }

        #endregion

        #region Command

        public Command<EmployeeModel> UpDateEmployeeCommand { get; set; }

        //Implementar Crear empleado
        public Command<EmployeeModel> CreateEmployeeCommand { get; set; }

        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            #region Set Command
            UpDateEmployeeCommand = new Command<EmployeeModel>(UpDateEmployee);
            CreateEmployeeCommand = new Command<EmployeeModel>(CreateEmployee);
            #endregion
        }
      
        private async void CreateEmployee(EmployeeModel model)
        {
            var employee = new Employee()
            {
                apellidos = model.apellidos,
                email = model.email,
                empresa = model.empresa,
                nombre = model.nombre,
                puesto = model.puesto,
                creation_date = DateTime.Now
            };
            #region Validaciones
            IsLoading = true;
            var valid = employee.DataAnotationsValid();
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

             
                var response = await _dataBaseService.CreateEmployee(employee);
                if (response.Success)
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", $"Usuario Agregado Exitosamente {employee}", "Aceptar");
                    IsCompletet = true;
                    MenuAdminPage.OnBackButton();
                    return;
                   
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", response.Message, "Ok");

                }

            IsLoading = false;
        }

        private async void UpDateEmployee(EmployeeModel employee)
        {
            IsLoading = true;

            #region Validaciones
            var valid = employee.DataAnotationsValid();
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



            var resp = await App.Current.MainPage.DisplayAlert("Employee Record", $"¿Estas seguro de actualizar la información el usuario {employee}?", "Aceptar", "Cancelar");
            if (resp)
            {
                var response = await _dataBaseService.UpdateEmployee(employee);
                if (response.Success)
                {
                    IsCompletet = true;
                    MenuAdminPage.OnBackButton();
                    return;
                }
            }
            IsLoading = false;
        }



        #endregion
    }
}
