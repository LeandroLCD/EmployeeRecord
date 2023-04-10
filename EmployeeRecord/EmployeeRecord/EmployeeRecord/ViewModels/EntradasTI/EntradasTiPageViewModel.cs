using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Register;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.EntradasTI
{
    public class EntradasTiPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<EmployeeModel> _employeeList;
        private EmployeeModel _employeeSelected;
        private ObservableCollection<TasksModel> _tasksList;
        private IDataBaseService _dataBaseService;
        private TasksModel _getTasks;

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
            set
            {
                //errorEmplead = string.Empty;
                SetProperty(ref _employeeSelected, value);
            }
        }

        public TasksModel GetTasks
        {
            get => _getTasks;
            set => SetProperty(ref _getTasks, value);
        }

        #endregion

        #region Command
        public Command RegisterCommand { get; set; }
        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private async void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();


            #region Set Command
            RegisterCommand = new Command(Register);
            #endregion

            #region Employees
            var employees = await _dataBaseService.GetEmployeeList();
            if (employees.Success)
            {
                EmployeeList = new ObservableCollection<EmployeeModel>((List<EmployeeModel>)employees.Objet);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", employees.Message, "Ok");

            }
            #endregion

            #region Tasks
            var tasks = await _dataBaseService.GetTasksList();
            if (tasks.Success)
            {

                TasksList = new ObservableCollection<TasksModel>((List<TasksModel>)tasks.Objet);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", tasks.Message, "Ok");

            }
            #endregion
        }

        private async void Register(object obj)
        {
            IsLoading = true;
            #region Validations
            if (EmployeeSelected == null)
            {
                //Agregar el error a la vista DisplayAlert//Propiedad publica de tipo string
                IsLoading = false;
                return;
            }
            if (GetTasks == null)
            {
                //Agregar el error a la vista
                IsLoading = false;
                return;
            }
            #endregion

            #region Insertar a la BD

            var employee = new EmployeeRegister()
            {
                hora_entra = DateTime.Now,
                motivo = GetTasks.Name,
                nombre = EmployeeSelected.nombre,
                apellidos = EmployeeSelected.apellidos,
                puesto = EmployeeSelected.puesto,
                empresa = EmployeeSelected.empresa,
                idEmpleado = EmployeeSelected.id

            };
            var register = await _dataBaseService.InsertRegisterIn(employee);



            #endregion

            if (register.Success)
            {
                GetTasks = new TasksModel();
                EmployeeSelected = new EmployeeModel();
                await App.Current.MainPage.DisplayAlert("Employee Record", register.Message, "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", register.Message, "OK");
            }

            IsLoading = false;
        }

        #endregion
    }
}

