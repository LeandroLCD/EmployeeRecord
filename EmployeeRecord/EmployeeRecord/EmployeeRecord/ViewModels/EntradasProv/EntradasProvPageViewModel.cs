using EmployeeRecord.Models.Autentication;
using EmployeeRecord.Models.Company;
using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Proveedor;
using EmployeeRecord.Models.Register;
using EmployeeRecord.Models.Tasks;
using EmployeeRecord.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.EntradasProv
{
    public class EntradasProvPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<TasksModel> _tasksList;
        private IDataBaseService _dataBaseService;
        private TasksModel _getTasks;
        private ObservableCollection<Company> _companyList;
        private TasksModel _taskSelected;
        private Company _companySelected;
        private ObservableCollection<Proveedor> _provedorList;
        private Proveedor _provedorSelected;
        private string _puesto;
        #endregion

        #region Constructor

        public EntradasProvPageViewModel()
        {
            InicializeProperties();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Muestra la lista de empresas en la vista (Picker)
        /// </summary>
        public ObservableCollection<Company> CompanyList
        {
            get => _companyList;
            set => SetProperty(ref _companyList, value);
        }

        public ObservableCollection<Proveedor> ProvedorList
        {
            get => _provedorList;
            set => SetProperty(ref _provedorList, value);
        }

        public string Puesto
        {
            get => _puesto;
            set => SetProperty(ref _puesto, value);
        }

        public Company CompanySelected
        {
            get => _companySelected;
            set => SetProperty(ref _companySelected, value);
        }

        public Proveedor ProvedorSelected
        {
            get => _provedorSelected;
            set => SetProperty(ref _provedorSelected, value);
        }


        /// <summary>
        /// Muestra la lista de tareas en la vista (Picker)
        /// </summary>
        public ObservableCollection<TasksModel> TasksList
        {
            get => _tasksList;
            set => SetProperty(ref _tasksList, value);
        }

        public TasksModel TaskSelected
        {
            get => _taskSelected;
            set => SetProperty(ref _taskSelected, value);
        }




        #endregion

        #region Command
        public Command RegisterCommand { get; set; }

        public Command CreateTaskCommand { get; set; }
               
        public Command CreateCompanyCommand { get; set; }
        public Command CreateProvedorCommand { get; private set; }
        #endregion

        #region Methods
        private async void  InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            #region Set Command
            RegisterCommand = new Command(Register);
            CreateTaskCommand = new Command(CreateTask);
            CreateCompanyCommand = new Command(CreateCompany);
            CreateProvedorCommand = new Command(CreateProvedor);
            #endregion

            GetCompany();

            GetTask();

            
        }

        private async void CreateProvedor(object obj)
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Agregar nuevo Proveedor", "Ingrese el nombre completo del proveedor", "Crear", "Cancelar");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                var provedor = await _dataBaseService.InsertProvedor(new Proveedor { provedor = name });
                if (provedor.Success)
                {
                    GetProvedor();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", provedor.Message, "Ok");
                }

            }
        }

        private async void CreateCompany(object obj)
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Crear nueva Empresa", "Ingrese el nombre de la empresa", "Crear", "Cancelar");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                var company = await _dataBaseService.InsertCompany(new Company { name = name });
                if (company.Success)
                {
                    GetCompany();  
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", company.Message, "Ok");
                }

            }
        }

        private async void CreateTask(object obj)
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Crear nueva Tarea", "Ingrese el nombre de tarea", "Crear", "Cancelar");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                var task = await _dataBaseService.InsertTask(new TasksModel { name = name });
                if (task.Success)
                {
                    GetTask();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Employee Record", task.Message, "Ok");
                }
            }
        }

        private async void Register(object obj)
        {
            #region Validaciones
            if (string.IsNullOrEmpty(ProvedorSelected.provedor))
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", "Debes seleccionar un proveedor", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(TaskSelected.name))
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", "Debes seleccionar una tarea", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(CompanySelected.name))
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", "Debes seleccionar una empresa", "Ok");
                return;
            }
            #endregion

            var proveedor = new ProveedorModel
            {
                puesto = Puesto,
                nombreCompleto = ProvedorSelected.provedor,
                motivo = TaskSelected.name,
                empresa = CompanySelected.name,
                hora_entra = DateTime.Now,
            };

            var register = await _dataBaseService.InsertRegisterProvIn(proveedor);
            if (register.Success)
            {
                ProvedorSelected = new Proveedor();
                TaskSelected = new TasksModel();
                CompanySelected = new Company();


                await App.Current.MainPage.DisplayAlert("Employee Record", register.Message, "OK");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", register.Message, "OK");
            }

            
        }
        #endregion

        private async void GetCompany()
        {
            #region Company
            var company = await _dataBaseService.GetCompanyList();
            if (company.Success)
            {
                CompanyList = new ObservableCollection<Company>((List<Company>)company.Objet);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", company.Message, "Ok");

            }
            #endregion
        }

        private async void GetTask()
        {
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

        private async void GetProvedor()
        {
            #region Proveedores
            var provedor = await _dataBaseService.GetProvedorList();
            if (provedor.Success)
            {

                ProvedorList = new ObservableCollection<Proveedor>((List<Proveedor>)provedor.Objet);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", provedor.Message, "Ok");

            }
            #endregion
        }
    }
}
