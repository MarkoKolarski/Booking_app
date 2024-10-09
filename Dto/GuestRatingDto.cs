using BookingApp.Model;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class GuestRatingDto : ViewModelBase
    {
        private readonly GuestRating _guestRating;
        private readonly Reservation _reservation;
        private readonly User _owner;

        public string CleanlinessRating => _guestRating.CleanlinessRating.ToString();
        public string RuleAdherenceRating => _guestRating.RuleAdherenceRating.ToString();
        public string AdditionalComment => _guestRating.AdditionalComment;
        public string RatingDate => _guestRating.RatingDate.ToString();


        public string AccommodationName => _reservation.Accommodation.Name;
        public string AccommodationPicture
        {
            get
            {
                if (_reservation.Accommodation.Pictures.Count > 0)
                {
                    return _reservation.Accommodation.Pictures[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string OwnerName => _owner.Username;
        public string ReservationStartDate => _reservation.ReservedDate.Item1.ToString("d");
        public string ReservationEndDate => _reservation.ReservedDate.Item2.ToString("d");

        public GuestRatingDto(GuestRating guestRating, Reservation reservation, User owner)
        {
            _guestRating = guestRating;
            _reservation = reservation;
            _owner = owner;
        }
    }
}
