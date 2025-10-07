using System;
using Windows.UI.Xaml.Data;

namespace CharityApp
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double d)
            {
                return d.ToString("F2");
            }
            return "0.00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (double.TryParse(value as string, out double result))
            {
                return result;
            }
            return 0.0;
        }
    }
}
