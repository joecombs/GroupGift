using System;
using System.Globalization;
using Xamarin.Forms;

namespace GroupGift.Helpers
{
    public class BooleanToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return "No"; }
            if ((value is bool && (bool)value)) { return "Yes"; }
            else { return "No"; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return false; }
            switch (value.ToString().ToLower())
            {
                case "y":
                    return true;
                default:
                    return false;
            }
        }
    }
}
