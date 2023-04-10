using EmployeeRecord.Models.Register;
using EmployeeRecord.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.SalidasTI
{
    public class SalidasTiPageViewModel : BaseViewModel
    {
        #region Fields
        private IDataBaseService _dataBaseService;
        private EmployeeRegister _getEmployee;
        private bool _isEnabledButton;
        private string _idEmpleado;
        private string _dateString;

        #endregion

        #region Constructor
        public SalidasTiPageViewModel()
        {
            InicializeProperties();
        }


        #endregion

        #region Properties
        public EmployeeRegister GetEmployee
        {
            get => _getEmployee;
            set => SetProperty(ref _getEmployee, value);

        }
        public string IdEmpleado
        {
            get => _idEmpleado;
            set => SetProperty(ref _idEmpleado, value);
        }


        public bool IsEnabledButton
        {
            get => _isEnabledButton;
            set => SetProperty(ref _isEnabledButton, value);
        }
        public string DateString
        {
            get => _dateString;
            set => SetProperty(ref _dateString, value);
        }

        #endregion

        #region Command
        public Command<string> SearchEmployeeCommand { get; set; }

        public Command ExitCommand { get; set; }
        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            #region Set Command
            SearchEmployeeCommand = new Command<string>(SearchEmployeeMethod);

            ExitCommand = new Command(ExitMethod);
            #endregion

            #region Employees

            #endregion


        }

        /// <summary>
        /// Metodo pàra marcar la salida del usuario
        /// </summary>
        /// <param name="obj"></param>
        private async void ExitMethod(object obj)
        {

            try
            {
                IsEnabledButton = false;
                if (GetEmployee != null && GetEmployee.id > 0)
                {
                    GetEmployee.hora_sali = DateTime.Now;
                    GetEmployee.IsExcited = true;
                    var resp = await _dataBaseService.UpdateRegisterOut(GetEmployee);
                    if (resp.Success)
                    {
                        GetEmployee = new EmployeeRegister();
                        IdEmpleado = DateString =  string.Empty;
                    }
                    else
                    {
                        //notificar al usuario
                    }
                }
            }
            catch (Exception)
            {
                //notificar al usuario
                return;
            }
        }

        /// <summary>
        /// Metodo para buscar el usuario por id
        /// </summary>
        /// <param name="id"></param>
        private async void SearchEmployeeMethod(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var resp = await _dataBaseService.GetRegisterIn(id);
                    if (resp.Success)
                    {
                        GetEmployee = (EmployeeRegister)resp.Objet;
                        DateString = _getEmployee.hora_entra.ToString();
                        IsEnabledButton = true;
                    }
                    else
                    {
                        //enviar el mensaje al usuario
                        IdEmpleado = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                //el error y notificar al usuario
                IsEnabledButton = false;
                return;
            }
        }



        #endregion
    }
}
