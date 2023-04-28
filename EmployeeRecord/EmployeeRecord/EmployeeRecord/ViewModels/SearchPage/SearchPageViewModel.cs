using EmployeeRecord.Models.Register;
using EmployeeRecord.Service.Interface;
using EmployeeRecord.ViewModels.ReportES;
using EmployeeRecord.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.SearchPage
{
    public class SearchPageViewModel: BaseViewModel
    {
        #region Fields
        private IDataBaseService _dataBaseService;
        private DateTime _getFecha_fin;
        private bool _isEnabled;
        private DateTime _getFecha_ini;

        #endregion

        #region constructor
        public SearchPageViewModel()
        {
            InicializeProperties();
        }

        
        #endregion

        #region Properties

        public DateTime GetFecha_ini
        {
            get => _getFecha_ini;
            set
            {
                if (value <= _getFecha_fin)
                    IsEnabledButton = true;
                else
                    IsEnabledButton = false;
                SetProperty(ref _getFecha_ini, value);
            }
        }

        public DateTime GetFecha_fin
        {
            get => _getFecha_fin;
            set
            {
                if(value >= _getFecha_ini)
                    IsEnabledButton = true;
                else
                    IsEnabledButton = false;
                SetProperty(ref _getFecha_fin, value);
            }
        }

        public bool IsEnabledButton
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }


        #endregion

        #region Command
        public Command SearchCommand { get; set; }
        #endregion

        #region Methods
        private void InicializeProperties()
        {
            GetFecha_fin = GetFecha_ini = DateTime.Now;
            _dataBaseService = DependencyService.Get<IDataBaseService>();

            SearchCommand = new Command(SearchRegister);
        }

        private async void SearchRegister(object obj)
        {
            if(_getFecha_fin >= _getFecha_ini)
            {
                var search = await _dataBaseService.SearchRegister(_getFecha_ini, _getFecha_fin);
                if (search.Success)
                {
                    var list = new List<RegisterEventModel>((List<RegisterEventModel>)search.Objet);
                    list.OrderBy(f => f.hora_entra);
                    if(list.Count == 0) 
                    {
                        //mensaje no existen datos para fecha de consulta
                        await App.Current.MainPage.DisplayAlert("Employee Record", "Nose encontro ningun registro con esas fechas", "Ok","Cancelar");
                        return;
                    }

                    ReportESViewModel.RegisterEvents = new LinkedList<RegisterEventModel>(list);

                    await Shell.Current.GoToAsync(nameof(ReportES));
                    
                }
                else
                {
                    // notificar error al usuario 
                    await App.Current.MainPage.DisplayAlert("Employee Record", "Error al buscar los eventos", "Ok", "Cancelar");
                }
            }
        }
        #endregion
    }
}
