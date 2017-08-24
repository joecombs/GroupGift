using System;
using System.Globalization;
using Xamarin.Forms;

namespace GroupGift.Helpers.Converters
{
    public class DateToDayString : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            if (value is DateTime)
            {
                DateTime d = (DateTime)value;
                return d.ToString("dddd, MMMM d, yyyy");
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
