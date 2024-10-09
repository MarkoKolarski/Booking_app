using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace BookingApp.Converter
{
    public class RatingToStarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string ratingString && int.TryParse(ratingString, out int rating) && rating >= 0 && rating <= 5)
            {
                List<Image> stars = new List<Image>();
                for (int i = 0; i < rating; i++)
                {
                    Image starImage = new Image();
                    starImage.Source = new BitmapImage(new Uri("../../Resources/Images/star.png", UriKind.Relative));
                    starImage.Width = 20;
                    starImage.Height = 20;
                    stars.Add(starImage);
                }
                for (int i = rating; i < 5; i++)
                {
                    Image starImage = new Image();
                    starImage.Source = new BitmapImage(new Uri("../../Resources/Images/star_empty.png", UriKind.Relative));
                    starImage.Width = 20;
                    starImage.Height = 20;
                    stars.Add(starImage);
                }
                return stars;
            }
            return Enumerable.Repeat(new Image() { Source = new BitmapImage(new Uri("../../Resources/Images/star_empty.png", UriKind.Relative)) }, 5).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}