using EmployeeRecord.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeRecord.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserListPage : ContentPage
	{
        private UserListPageViewModel _viewModel;

        public UserListPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            employeeList.SelectedItem = null;
            _viewModel = (UserListPageViewModel)BindingContext;
            _viewModel.Actualize();
            base.OnAppearing();
        }
    }
}