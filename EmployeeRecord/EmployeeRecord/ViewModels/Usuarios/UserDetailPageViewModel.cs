﻿using EmployeeRecord.Models.Autentication;
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
        private Employee _employeeSelected;
        public static Employee GetEmployee { get; set; }
        private IDataBaseService _dataBaseService;

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
                EmployeeSelected = new Employee();
            }
            InicializeProperties();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Muestra la lista de los empleados en la vista (Picker)
        /// </summary> 

        public Employee EmployeeSelected
        {
            get => _employeeSelected;
            set => SetProperty(ref _employeeSelected, value);
        }

        #endregion

        #region Command

        public Command<Employee> UpDateEmployeeCommand { get; set; }

        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            #region Set Command
            UpDateEmployeeCommand = new Command<Employee>(UpDateEmployee);
            #endregion
        }

        private async void UpDateEmployee(Employee employee)
        {
            IsLoading = true;
            var resp = await App.Current.MainPage.DisplayAlert("Employee Record", $"¿Estas seguro de adtualizar la información el usuario {employee}?", "Aceptar", "Cancelar");
            if (resp)
            {
                var response = await _dataBaseService.UpdateEmployee(employee);
                if (response.Success)
                {
                    IsCompletet = true;
                    AdminShellPage.OnBackButton();
                    return;
                }
            }
            IsLoading = false;
        }

        

        #endregion
    }
}