using System;
using Windows.UI.Xaml.Data;

namespace CharityApp
{
    public class StatusToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is BoxStatus status)
            {
                switch (status)
                {
                    case BoxStatus.NotPicked:
                        return "niepobrana";
                    case BoxStatus.Picked:
                        return "pobrana";
                    case BoxStatus.Returned:
                        return "zwrócona";
                    case BoxStatus.Settled:
                        return "rozliczona";
                    default:
                        return "Unknown Status";
                }
            }
            return "Unknown2";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
