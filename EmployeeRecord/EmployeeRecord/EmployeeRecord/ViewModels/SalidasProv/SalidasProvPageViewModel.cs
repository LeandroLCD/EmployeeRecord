using EmployeeRecord.Models.Register;
using EmployeeRecord.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.SalidasProv
{
    public class SalidasProvPageViewModel: BaseViewModel
    {
        #region Fields
        private IDataBaseService _dataBaseService;
        private ProveedorModel _getProveedor;
        private bool _isEnabledButton;
        private ProveedorModel _proveedorSelect;
        private string _dateString;
        private ObservableCollection<ProveedorModel> _getProveedorsList;
        private int _id;

        #endregion

        #region Constructor
        public SalidasProvPageViewModel()
        {
            InicializeProperties();
        }


        #endregion

        #region Properties

        public ObservableCollection<ProveedorModel> GetProveedorsList
        {
            get => _getProveedorsList;
            set => SetProperty(ref _getProveedorsList, value);
        }
        public int Id
        {
            get => _id;

            set 
            {
                if (value >= 0 && GetProveedorsList != null)
                    ProveedorSelected = GetProveedorsList.ElementAt(value);
                SetProperty(ref _id, value); 
            }
        }
     
        public ProveedorModel ProveedorSelected
        {
            get => _proveedorSelect;
            set
            {
                if (value == null)
                {
                    value = new ProveedorModel()
                    {
                        nombre = string.Empty,
                        apellidos = string.Empty,
                        empresa = string.Empty,
                        motivo = string.Empty,
                    };
                    DateString = string.Empty;
                } 
                else
                {
                    IsEnabledButton = true;
                    DateString = value.hora_entra.ToString();
                }
                    
                  
                SetProperty(ref _proveedorSelect, value);
            }

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

        public Command ExitCommand { get; set; }
        #endregion

        #region Methodos
        /// <summary>
        /// Inicializa las propiedades del Viewmodel
        /// </summary>
        private async void InicializeProperties()
        {
            _dataBaseService = DependencyService.Get<IDataBaseService>();
            #region Set Command
            
            ExitCommand = new Command(ExitMethod);
            #endregion

            #region Proveedores
            _id = -1;
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
                if (Id >= 0)
                {
                    
                    ProveedorSelected.hora_sali = DateTime.Now;
                    ProveedorSelected.IsExcited = true;
                    var resp = await _dataBaseService.UpdateProvedorOut(ProveedorSelected);
                    if (resp.Success)
                    {
                        GetProveedorsList.RemoveAt(Id);
                        NotifyPropertyChanged(nameof(GetProveedorsList));
                        ProveedorSelected = null;
                        Id = -1;
                    }
                    else
                    {
                        IsEnabledButton = true;
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

        private async void LoadProv()
        {
            var proveedors = await _dataBaseService.GetProveedorIn();
            if (proveedors.Success)
            {
                GetProveedorsList = new ObservableCollection<ProveedorModel>((List<ProveedorModel>)proveedors.Objet);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Employee Record", proveedors.Message, "OK");
            }
        }

        public void Actualizate()
        {
            LoadProv();
        }




        #endregion
    }
}
