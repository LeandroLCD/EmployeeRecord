using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.EntradasTI
{
    public class EntradasTiPageViewModel: BaseViewModel
    {
        #region Fields
        private ObservableCollection<EmployeeModel> _employeeList;
        private EmployeeModel _employeeSelected;
        private ObservableCollection<TasksModel> _tasksList;
        private IDataBaseService _dataBaseService;

        #endregion

        #region Constructor
        public EntradasTiPageViewModel()
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
        /// <summary>
        /// Muestra la lista de tareas en la vista (Picker)
        /// </summary>
        public ObservableCollection<TasksModel> TasksList 
        {
            get => _tasksList;
            set => SetProperty(ref _tasksList, value);
        }
        /// <summary>
        /// Contiene el Empleado seleccionado
        /// </summary>
        public EmployeeModel EmployeeSelected
        {
            get => _employeeSelected;
            set => SetProperty(ref _employeeSelected, value);
        }

        #endregion

        #region Command

        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private async void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            #region Employees
            var resp = await _dataBaseService.GetEmployeeList();
            if(resp.Success)
            {
                EmployeeList = new ObservableCollection<EmployeeModel>((List<EmployeeModel>)resp.Objet);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", resp.Message,"Ok");

            }
            #endregion

            #region Tasks
            //var resp2 = await _dataBaseService.GetTasksList();
            //if (resp.Success)
            //{
            //    TasksList = new ObservableCollection<TasksModel>((List<TasksModel>)resp.Objet);
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Employee Record", resp.Message, "Ok");

            //}
            #endregion
        }

        #endregion
    }
}
