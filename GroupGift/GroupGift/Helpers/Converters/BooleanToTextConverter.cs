using System;
using System.Globalization;
using Xamarin.Forms;

namespace GroupGift.Helpers.Converters
{
    public class BooleanToTextConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "No";
            if (value is bool)
            {
                bool b = (bool)value;
                return b ? "Yes" : "No";
            }
            else {
                throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
