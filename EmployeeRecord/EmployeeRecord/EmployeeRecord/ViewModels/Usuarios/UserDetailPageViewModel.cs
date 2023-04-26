using EmployeeRecord.Models.Employees;
using EmployeeRecord.Service.Interface;
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

        private async void CreateEmployee(EmployeeModel employee)
        {
            /*Revision del metodo CreateEmployee
             1. Este metodo como todo metodo debe validar el parametro que recibe por el contructor.
                antes de enviar a realizar las acciones a la database.
                Nota: como el modelo "EmployeeModel" que es el tipo de dato recibido desde la vista no 
                contiene etiquetas de validacion ni metodo de extención para validarse, tienes 2 opciones para
                realizar eso. A)se puede parcear el EmployeModel a Employe que si tiene etiquetas de validacion 
                y metodo de extencion para validar: Ej var model = new Employe{ nombre = employee.nombre} y asi con
                todas las propiedades y despues aplicar la validación puedes usar como ejemplo el metodo register
                que se aplican dichas validaciones de manera correpta.
            2. No has creado el metodo CreateEmployee en la interfaz IDataBaseService e implementarlo en DataBaseService
                para que puedas mandar el empleado a la base de datos este metodo debe recibir un Employe que es el parametro que
                se esta evaluando en el paso anterior. en El modelo Empoyee debes crear un metodo que retorne el Query
                para insertar dichos datos a la base de datos. Este metodo CreateEmployee es igual que el InsertTask lo que cambia
                en el parametro que recibe y el query. Realizando eso podras insertar de manera correpta a la base de datos.
            3. Evaluar la respuesta como esta el if(response.Success) esta bien falta el else, para cuando falle la inserción retorne
                el mensaje al usuario, el mensaje que estas enviando debajo del IsLoanding esta mal debes moverlo al if(response.susscee)
                antes del IsComplet y debe quedar asi: await App.Current.MainPage.DisplayAlert("Employee Record", $"Usuario Agregado Exitosamente {employee}", "Aceptar");
                porque un mensaje de notificacion y no de pregunat como esta hay abajo.
             
             
             */
            IsLoading = true;
            var resp = await App.Current.MainPage.DisplayAlert("Employee Record", $"Usuario Agregado Exitosamente {employee}", "Aceptar", "Cancelar");
            if (resp)
            {
                var response = await _dataBaseService.CreateEmployee(employee);
                if (response.Success)
                {
                    IsCompletet = true;
                    MenuAdminPage.OnBackButton();
                    return;
                }
            }
            IsLoading = false;
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
