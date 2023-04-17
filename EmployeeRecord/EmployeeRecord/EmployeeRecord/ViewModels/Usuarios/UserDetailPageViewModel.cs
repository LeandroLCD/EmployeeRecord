using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.Usuarios
{
    public class UserDetailPageViewModel: BaseViewModel
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
            set=> SetProperty(ref _isCreate, value);
        }

        #endregion

        #region Command

        public Command<EmployeeModel> UpDateEmployeeCommand { get; set; }

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
            #endregion
        }

        private async void UpDateEmployee(EmployeeModel employee)
        {
            IsLoading = true;
            var resp = await App.Current.MainPage.DisplayAlert("Employee Record", $"¿Estas seguro de adtualizar la información el usuario {employee}?", "Aceptar", "Cancelar");
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
