using EmployeeRecord.Models.Employees;
using EmployeeRecord.Models.Register;
using EmployeeRecord.Service.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmployeeRecord.ViewModels.ReportES
{
    public class ReportESViewModel: BaseViewModel
    {
        #region Fields
        private ObservableCollection<RegisterEventModel> _getEvents;
        private LinkedList<RegisterEventModel> _registerEvents { get; set; }

        #endregion

        #region Constructor
        public ReportESViewModel()
        {
            InicializeProperties();
        }

      
        #endregion

        #region Command

        public Command<string> SearchBarCommand { get; set; }

        public Command<RegisterEventModel> ReportToPdfCommand { get; set; }

        #endregion

        #region Properties
        public static LinkedList<RegisterEventModel> RegisterEvents { get; set; }

        public ObservableCollection<RegisterEventModel> GetEventsList
        {
            get => _getEvents;
            set=>SetProperty(ref _getEvents, value);
        }

        #endregion

        #region Methods
        private void InicializeProperties()
        {
            if (RegisterEvents != null)
            {
                GetEventsList = new ObservableCollection<RegisterEventModel>(RegisterEvents);
                _registerEvents = RegisterEvents;
                RegisterEvents = null;
            }

            SearchBarCommand = new Command<string>(SearchBarChanged);

            ReportToPdfCommand = new Command<RegisterEventModel>(ReportToPdf);
        }

        private async void ReportToPdf(RegisterEventModel model)
        {
            if(model != null)
            {
                var path = Reports.ToPdf($"Reporte_{model.nombre}_{model.apellidos}", model);
                await Share.RequestAsync(new ShareFileRequest

                {
                    Title = "Elegir App para exportar reporte",
                    File = new ShareFile(path)
                });

            }
        }

        private void SearchBarChanged(string txt)
        {
            try
            {

                if (!string.IsNullOrEmpty(txt))
                {
                    GetEventsList = new ObservableCollection<RegisterEventModel>(_registerEvents.Where(c => c.nombre.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.apellidos.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.motivo.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.empresa.ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.hora_entra.ToString().ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    || c.hora_sali.ToString().ToLowerInvariant().Contains(txt.ToLowerInvariant())
                    ));
                    return;
                }
                GetEventsList = new ObservableCollection<RegisterEventModel>(_registerEvents);
            }
            catch (Exception)
            {

                return;
            }
        }
        #endregion

    }
}
