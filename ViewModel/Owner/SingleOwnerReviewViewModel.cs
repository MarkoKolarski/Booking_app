using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    class SingleOwnerReviewViewModel : ViewModelBase
    {
        private readonly AccommodationRatingService _accommodationRatingService;
        private AccommodationRatingDto _rating;
        private int currentIndex = 0;
        public AccommodationRatingDto Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(AccommodationRating));
            }
        }

        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get { return _currentImage; }
            set { _currentImage = value; OnPropertyChanged(nameof(CurrentImage)); }
        }
        public RelayCommand NextImageCommand { get; set; }
        public RelayCommand PreviousImageCommand { get; set; }
        public RelayCommand GetBack { get; set; }
        public NavigationService NavService { get; set; }
        public SingleOwnerReviewViewModel(int ratingId, NavigationService navService)
        {
            _accommodationRatingService = new AccommodationRatingService();
            NavService = navService;
            LoadRating(ratingId);
            NextImageCommand = new RelayCommand(NextImage);
            PreviousImageCommand = new RelayCommand(PreviousImage);
            ShowImage();
            GetBack = new RelayCommand(GetBackAction);
        }

        private void ShowImage()
        {
            if (currentIndex >= 0 && currentIndex < Rating.GuestImages.Count)
            {
                string imagePath = Rating.GuestImages[currentIndex];
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                CurrentImage = bitmap;
            }
        }

        private void NextImage(object parameter)
        {
            currentIndex = (currentIndex + 1) % Rating.GuestImages.Count;
            ShowImage();
        }

        private void PreviousImage(object parameter)
        {
            currentIndex = (currentIndex - 1 + Rating.GuestImages.Count) % Rating.GuestImages.Count;
            ShowImage();
        }
        public void LoadRating(int ratingId)
        {
            AccommodationRating rating = _accommodationRatingService.GetById(ratingId);
            Rating = ConvertToDTO(rating);
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

        private void GetBackAction(object parameter)
        {
            NavService.GoBack();
        }

    }
}
