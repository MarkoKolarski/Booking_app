using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.Converter
{
    public class RenovationLevelToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RenovationLevel level)
            {
                switch (level)
                {
                    case RenovationLevel.None:
                        return "Izaberite nivo hitnosti renoviranja";
                    case RenovationLevel.Level1:
                        return "Nivo 1- bilo bi lepo renovirati neke sitnice,\nali sve funkcioniše kako treba i bez toga";
                    case RenovationLevel.Level2:
                        return "Nivo 2- male zamerke na smeštaj\nkoje kada bi se uklonile bi ga učinile savršenim";
                    case RenovationLevel.Level3:
                        return "Nivo 3- nekoliko stvari koje su baš zasmetale\nbi trebalo renovirati";
                    case RenovationLevel.Level4:
                        return "Nivo 4- ima dosta loših stvari\ni renoviranje je stvarno neophodno";
                    case RenovationLevel.Level5:
                        return "Nivo 5- smeštaj je u jako lošem stanju\ni ne vredi ga uopšte iznajmljivati ukoliko se ne renovira";
                    default:
                        return value.ToString();
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (stringValue == "Izaberite\npreporuku")
                {
                    return RenovationLevel.None;
                }
                else
                {
                    return Enum.Parse(typeof(RenovationLevel), stringValue.Replace("\n", " "));
                }
            }
            return RenovationLevel.None;
        }
    }
}
