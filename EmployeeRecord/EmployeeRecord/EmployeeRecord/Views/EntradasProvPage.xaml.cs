﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeRecord.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntradasProvPage : ContentPage
    {
        public EntradasProvPage()
        {
            InitializeComponent();

            Puesto.Text = "Proveedor";
        }
    }
}