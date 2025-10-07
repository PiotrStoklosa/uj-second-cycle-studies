using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CharityApp
{
    public class StatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is BoxStatus status && status == BoxStatus.Settled ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
