using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BookingApp.ViewModel.Guest
{
    public class OwnerRatingsViewModel : ViewModelBase
    {
        private readonly GuestRatingService _guestRatingService;
        private readonly AccommodationRatingService _accommodationRatingService;
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;


        public User User { get; set; }
        public ObservableCollection<GuestRatingDto> _guestRatings { get; set; }

        public OwnerRatingsViewModel(User user)
        {
            User = user;
            _guestRatingService = new GuestRatingService();
            _accommodationRatingService = new AccommodationRatingService();
            _reservationService = new ReservationService();
            _userService = new UserService();
            LoadGuestRatings();
        }

        private void LoadGuestRatings()
        {
            var guestRatings = _accommodationRatingService.GetAllByGuestId(User.Id);
            var ownerRatings = _guestRatingService.GetAll();
            var filteredRatings = ownerRatings.Where(or => guestRatings.Any(gr => gr.ReservationId == or.ReservationId)).ToList();

            var GuestRatingDtos = new List<GuestRatingDto>();

            foreach (var rating in filteredRatings)
            {
                var reservation = _reservationService.GetById(rating.ReservationId);
                var owner = _userService.FindById(rating.OwnerID);

                var GuestRatingDto = new GuestRatingDto(rating, reservation, owner);

                GuestRatingDtos.Add(GuestRatingDto);
            }

            _guestRatings = new ObservableCollection<GuestRatingDto>(GuestRatingDtos);
        }
    }
}