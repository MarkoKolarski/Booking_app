using BookingApp.Converter;
using BookingApp.Model;
using BookingApp.Service;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class RateAccommodationViewModel : ViewModelBase
    {
        private readonly AccommodationRatingService _accommodationRatingService;
        private readonly ReservationService _reservationService;
        private Reservation _selectedReservation;
        private AccommodationRating _rating;

        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
            }
        }

        private int _currentImageIndex = 0;
        public int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            set
            {
                if (SelectedAccommodation != null && SelectedAccommodation.Pictures.Count > 0)
                {
                    _currentImageIndex = value;
                    if (_currentImageIndex < 0)
                    {
                        _currentImageIndex = SelectedAccommodation.Pictures.Count - 1;
                    }
                    else if (_currentImageIndex >= SelectedAccommodation.Pictures.Count)
                    {
                        _currentImageIndex = 0;
                    }
                    OnPropertyChanged(nameof(CurrentImageIndex));
                    OnPropertyChanged(nameof(CurrentImagePath));
                }
            }
        }

        public string CurrentImagePath
        {
            get
            {
                if (SelectedAccommodation != null && SelectedAccommodation.Pictures.Count > 0)
                {
                    return SelectedAccommodation.Pictures[_currentImageIndex];
                }
                return null;
            }
        }

        private RenovationLevel? _selectedRenovationLevel = RenovationLevel.None;
        public RenovationLevel? SelectedRenovationLevel
        {
            get { return _selectedRenovationLevel; }
            set
            {
                _selectedRenovationLevel = value;
                OnPropertyChanged(nameof(SelectedRenovationLevel));
                Rating.RenovationLevel = _selectedRenovationLevel;
            }
        }

        public RenovationLevel[] RenovationLevels { get; } = (RenovationLevel[])Enum.GetValues(typeof(RenovationLevel));

        public User User { get; set; }

        public AccommodationRating Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        private ObservableCollection<string> _guestImages = new ObservableCollection<string>();
        public ObservableCollection<string> GuestImages
        {
            get { return _guestImages; }
            set
            {
                _guestImages = value;
                OnPropertyChanged(nameof(GuestImages));
                Rating.GuestImages = _guestImages.ToList();
            }
        }

        public ICommand FocusCommand { get; private set; }

        public RateAccommodationViewModel(Accommodation selectedAccommodation, User user, Reservation selectedReservation)
        {
            _selectedReservation = selectedReservation;
            _selectedAccommodation = selectedAccommodation;
            User = user;
            _rating = new AccommodationRating();
            _rating.ReservationId = selectedReservation.Id;
            _rating.Guest = User;
            _rating.Accommodation = _selectedAccommodation;
            _accommodationRatingService = new AccommodationRatingService();
            _reservationService = new ReservationService();

            FocusCommand = new RelayCommand(ExecuteFocusCommand);

        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public void PreviousImage()
        {
            CurrentImageIndex--;
        }

        public void NextImage()
        {
            CurrentImageIndex++;
        }

        private bool GatherRatingData()
        {
            return ValidateComment() && ValidateCleanliness() && ValidateOwnerCorrectness() && CheckAccommodationReservation();
        }

        private bool Validate(Func<bool> predicate, string errorMessage)
        {
            if (predicate())
            {
                ShowErrorMessage(errorMessage);
                return false;
            }
            return true;
        }

        private bool ValidateComment()
        {
            return Validate(() => string.IsNullOrWhiteSpace(Rating.Comment), "Unesite komentar.");
        }

        private bool ValidateCleanliness()
        {
            return Validate(() => Rating.Cleanliness == 0, "Morate dati ocenu za čistoću.");
        }

        private bool ValidateOwnerCorrectness()
        {
            return Validate(() => Rating.OwnerCorrectness == 0, "Morate dati ocenu za korektnost vlasnika.");
        }


        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool CheckAccommodationReservation()
        {
            if (_selectedAccommodation.Reservations == null || !_selectedAccommodation.Reservations.Any())
            {
                ShowErrorMessage("Prvo morate imati iskustva sa smeštajem da biste ga ocenili.");
                return false;
            }
            return true;
        }

        public bool SubmitRating()
        {
            if (!GatherRatingData())
            {
                return false;
            }
            Rating.SuggestionDate = DateTime.Now;

            _accommodationRatingService.SaveRating(Rating);


            _selectedReservation.AccommodationRating = Rating;

            _reservationService.Update(_selectedReservation);




            MessageBox.Show("Hvala na ocenjivanju!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }


        public void BrowseImages()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog() == true)
            {
                foreach (string filename in dialog.FileNames)
                {
                    if (!_guestImages.Contains(filename))
                    {
                        _guestImages.Add(filename);
                    }
                }

                OnPropertyChanged(nameof(GuestImages));
                Rating.GuestImages = _guestImages.ToList();
            }
        }
    }
}