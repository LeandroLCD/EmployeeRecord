using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Register;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.Usuarios
{
    public class UserListPageViewModel: BaseViewModel
    {
        #region Fields
        private ObservableCollection<EmployeeModel> _employeeList;
        private List<EmployeeModel> GetEmployees { get; set; }
        private Employee _employeeSelected;
        private IDataBaseService _dataBaseService;

        #endregion

        #region Constructor
        public UserListPageViewModel()
        {
            InicializeProperties();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Muestra la lista de los empleados en la vista (Picker)
        /// </summary> 

        public ObservableCollection<EmployeeModel> EmployeeList
        {
            get => _employeeList;
            set => SetProperty(ref _employeeList, value);
        }

        #endregion

        #region Command
        public Command<EmployeeModel> EmployeeDetailCommand { get; set; }

        public Command CreateCommand { get; set; }
        public Command<EmployeeModel> DeleteEmployeeCommand { get; set; }

        public Command<string> SearchBarCommand { get; set; }
        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private async void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();


            #region Set Command
            EmployeeDetailCommand = new Command<EmployeeModel>(async (employee) => {
                if(employee != null)
                {
                    UserDetailPageViewModel.GetEmployee = employee;
                    await Shell.Current.GoToAsync(nameof(UserDetailPage));
                }
            });

            CreateCommand = new Command(async () => {
                await Shell.Current.GoToAsync(nameof(UserDetailPage));
            });
            SearchBarCommand = new Command<string>(SearchBarChanged);

            DeleteEmployeeCommand = new Command<EmployeeModel>(DeleteEmployee);

            #endregion

            #region Employees
            var employees = await _dataBaseService.GetEmployeeZmotors();
            if (employees.Success)
            {
                GetEmployees = (List<EmployeeModel>)employees.Objet;
                EmployeeList = new ObservableCollection<EmployeeModel>(GetEmployees);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", employees.Message, "Ok");

            }
            #endregion

           
        }

        private void SearchBarChanged(string txt)
        {
            try
            {

                if (!string.IsNullOrEmpty(txt))
                {
                    EmployeeList = new ObservableCollection<EmployeeModel>(GetEmployees.Where(c => c.nombre.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.apellidos.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.puesto.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.empresa.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.email.ToLowerInvariant().Contains(txt.ToLowerInvariant())

                    ));
                    return;
                }
                EmployeeList = new ObservableCollection<EmployeeModel>(GetEmployees);
            }
            catch (Exception)
            {

                return;
            }
        }

        private async void DeleteEmployee(EmployeeModel employee)
        {
            var resp = await App.Current.MainPage.DisplayAlert("Employee Record", $"¿Estas seguro de eliminar el usuario {employee}?", "Aceptar", "Cancelar");
            if (resp)
            {
                var response = await _dataBaseService.DeleteEmployee(employee);
                if (response.Success)
                {

                    GetEmployees.Remove(employee);
                    EmployeeList = new ObservableCollection<EmployeeModel>(GetEmployees);
                }
            }



        }

        public async void Actualize()
        {
            if (IsCompletet)
            {
                IsCompletet = false;
                var employees = await _dataBaseService.GetEmployeeZmotors();
                if (employees.Success)
                {
                    GetEmployees = (List<EmployeeModel>)employees.Objet;
                    EmployeeList = new ObservableCollection<EmployeeModel>(GetEmployees);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", employees.Message, "Ok");

                }
            }
        }



        #endregion
    }
}
