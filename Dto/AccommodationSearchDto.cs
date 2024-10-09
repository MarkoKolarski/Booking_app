using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;


namespace BookingApp.Dto
{
    public class AccommodationSearchDto : ViewModelBase
    {
        private readonly Accommodation _accommodation;

        public string Id => _accommodation.Id.ToString();
        public string Name => _accommodation.Name;
        public string City => _accommodation.Location.City;
        public string Country => _accommodation.Location.Country;
        public string Type => _accommodation.Type.ToString();
        public string MaxGuests => _accommodation.MaxGuests.ToString();
        public string MinReservationDays => _accommodation.MinReservationDays.ToString();
        public string Picture
        {
            get
            {
                if (_accommodation.Pictures.Count > 0)
                {
                    return _accommodation.Pictures[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public AccommodationSearchDto(Accommodation accommodation)
        {
            _accommodation = accommodation;
        }
    }
}
