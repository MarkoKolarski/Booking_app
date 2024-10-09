using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string dateString)
            {
                return DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);
            }
            else if (value is DateTime dateTime)
            {
                return dateTime.ToString("dd/MM/yyyy");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string dateRangeString)
            {
                string[] parts = dateRangeString.Split(" - ");
                if (parts.Length == 2)
                {
                    DateTime startDate = DateTime.ParseExact(parts[0], "dd/MM/yyyy", culture);
                    return startDate;
                }
            }
            else if (value is DateTime dateTime)
            {
                return dateTime.ToString("dd/MM/yyyy");
            }
            return value;
        }
    }
}