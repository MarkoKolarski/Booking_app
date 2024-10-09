using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BookingApp.Converter
{
    public class StatusToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length > 0 && values[0] is RequestStatus status)
            {
                switch (status)
                {
                    case RequestStatus.Neodlučeno:
                        return new SolidColorBrush(Color.FromRgb(255, 255, 102)); // Žuta boja
                    case RequestStatus.Odobreno:
                        return new SolidColorBrush(Color.FromRgb(102, 255, 102)); // Zelena boja
                    case RequestStatus.Odbijeno:
                        return new SolidColorBrush(Color.FromRgb(255, 102, 102)); // Crvena boja
                    default:
                        return new SolidColorBrush(Color.FromRgb(255, 255, 255)); // Bijela boja
                }
            }

            return new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
