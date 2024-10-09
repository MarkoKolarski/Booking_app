using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.ViewModel;

namespace BookingApp.Dto
{
    public class ReservationDto : ViewModelBase
    {
        private readonly Reservation _reservation;

        public int Id => _reservation.Id;
        public string AccommodationName => _reservation.Accommodation?.Name ?? string.Empty;
        public string CheckInDate => _reservation.ReservedDate?.Item1.ToString("b") ?? default;
        public string CheckOutDate => _reservation.ReservedDate?.Item2.ToString("b") ?? default;
        public string NumberOfGuests => _reservation.NumberOfGuests.ToString();
        public string AccommodationRating => _reservation.AccommodationRating?.ToString() ?? string.Empty;
        public string Deleted => _reservation.Deleted.ToString();

        public ReservationDto(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}