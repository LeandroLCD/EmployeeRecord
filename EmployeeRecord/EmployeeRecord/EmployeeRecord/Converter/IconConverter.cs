using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EmployeeRecord.Converter
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if(isChecked)
            {
                return "ic_eye_off.png";
            }
            else
            {
                return "ic_eye.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           var img = (string)value;
            if (img == "ic_eye-off.png")
                return true;
            else
                return false;
        }
    }
}
