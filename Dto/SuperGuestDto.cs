using BookingApp.Model;
using BookingApp.ViewModel;
using System;

namespace BookingApp.Dto
{
    public class SuperGuestDto : ViewModelBase
    {
        private readonly SuperGuest _superGuest;

        public int ReservationCount => _superGuest.ReservationCount;
        public bool IsSuperGuest => _superGuest.IsSuperGuest;
        public int BonusPoints => _superGuest.BonusPoints;
        public string StartDate => _superGuest.StartDate?.ToString("dd.MM.yyyy");

        public SuperGuestDto(SuperGuest superGuest)
        {
            _superGuest = superGuest;
        }
    }
}
