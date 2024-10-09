using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    public class OwnerReviewsViewModel
    {

        private readonly AccommodationRatingService _accommodationRatingService;
        private readonly GuestRatingService _guestRatingService;
        private readonly ObservableCollection<AccommodationRatingDto> _ratings;
        public ObservableCollection<AccommodationRatingDto> Ratings => _ratings;
        public ObservableCollection<AccommodationRating> AllRatings { get; set; }
        public ObservableCollection<AccommodationRating>? OwnerRatings { get; set; }
        public NavigationService NavService { get; set; }

        private User _currentUser { get; set; }

        public RelayCommand ShowReviewDetails { get; set; }
        public RelayCommand GetBack { get; set; }
        public OwnerReviewsViewModel(User user, NavigationService navService)
        {
            _accommodationRatingService = new AccommodationRatingService();
            _guestRatingService = new GuestRatingService();
            NavService = navService;
            AllRatings = new ObservableCollection<AccommodationRating>();
            _currentUser = user;
            ShowAllReviews();
            _ratings = new ObservableCollection<AccommodationRatingDto>(OwnerRatings.Select(rating => ConvertToDTO(rating)));
            ShowReviewDetails = new RelayCommand(ShowReviewDetailsExecute);
            GetBack = new RelayCommand(GetBackAction);
        }


        private AccommodationRatingDto ConvertToDTO(AccommodationRating rating)
        {
            return new AccommodationRatingDto(
                rating.Id,
                rating.Cleanliness,
                rating.OwnerCorrectness,
                rating.Comment,
                rating.Guest.Username,
                rating.Accommodation.Name,
                new ObservableCollection<string>(rating.GuestImages)
            );
        }
        private void ShowAllReviews()
        {
            AllRatings = new ObservableCollection<AccommodationRating>(_accommodationRatingService.GetAllByOwnerId(_currentUser.Id));
            OwnerRatings = new ObservableCollection<AccommodationRating>(AllRatings.Where(rating => _guestRatingService.ExistByGuestAndOwnerId(rating.Guest.Id, _currentUser.Id)));
        }

        private void ShowReviewDetailsExecute(object parameter)
        {
            AccommodationRatingDto rating = (AccommodationRatingDto)parameter;
            SingleOwnerReviewView singleOwnerReview = new SingleOwnerReviewView(rating.Id, NavService);
            NavService.Navigate(singleOwnerReview);
        }

        private void GetBackAction(object parameter)
        {
            NavService.GoBack();
        }
    }
}