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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            App.GlobalNavigation.PopToRootAsync();
            return true;
        }

        private void Clean_Clicked(object sender, EventArgs e)
        {
            email.Text = string.Empty;
        }
    }
    
}