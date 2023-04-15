using EmployeeRecord.Models.Register;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
            if(RegisterEvents != null)
            {
                GetEvents = new ObservableCollection<RegisterEventModel>(RegisterEvents);
                _registerEvents = RegisterEvents;
                RegisterEvents = null;
            }
        }
        #endregion

        #region Properties
        public static LinkedList<RegisterEventModel> RegisterEvents { get; set; }

        public ObservableCollection<RegisterEventModel> GetEvents
        {
            get => _getEvents;
            set=>SetProperty(ref _getEvents, value);
        }

        #endregion

    }
}
