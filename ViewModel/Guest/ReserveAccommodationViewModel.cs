using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guest;
using BookingApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class ReserveAccommodationViewModel : ViewModelBase
    {
        private readonly ReservationService _reservationService;
        private readonly SuperGuestService _superGuestService;
        public ObservableCollection<DateTime> AvailableDates { get; set; }
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
        private DateTime _startDate = DateTime.Today.AddDays(1);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        private DateTime _endDate = DateTime.Today.AddDays(2);
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        private int? _numberOfGuests;
        public int? NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                _numberOfGuests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }
        private int? _numberOfDays;
        public int? NumberOfDays
        {
            get => _numberOfDays;
            set
            {
                _numberOfDays = value;
                OnPropertyChanged(nameof(NumberOfDays));
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

        public User User { get; set; }
        public ICommand FocusCommand { get; private set; }

        public ReserveAccommodationViewModel(Accommodation selectedAccommodation, User user)
        {
            _selectedAccommodation = selectedAccommodation;
            User = user;
            _reservationService = new ReservationService();
            _superGuestService = new SuperGuestService();
            _selectedAccommodation.Reservations = _reservationService.GetReservationsByAccommodationId(_selectedAccommodation.Id);
            AvailableDates = new ObservableCollection<DateTime>();

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

        public void ReserveAccommodation()
        {
            if (!ValidateInput()) return;

            List<DateTime> availableDates = GetAvailableDates();

            var superGuest = _superGuestService.GetByGuestId(User.Id);
            if (superGuest == null)
            {

                superGuest = new SuperGuest();
                superGuest.GuestId = User.Id;
                _superGuestService.Save(superGuest);
            }

            if (availableDates.Count == 0)
            {
                HandleUnavailableDates();
                return;
            }

            ReserveSelectedDates(availableDates);

        }

        private List<DateTime> GetAvailableDates()
        {
            return ReservationUtils.GetAvailableDates(_selectedAccommodation, StartDate, EndDate, NumberOfDays);
        }

        private void HandleUnavailableDates()
        {
            List<DateTime> alternativeDates = ReservationUtils.FindAlternativeDates(_selectedAccommodation, StartDate, NumberOfDays);
            if (alternativeDates.Any())
            {
                if (UserAcceptsAlternativeDates(alternativeDates))
                {
                    ReserveSelectedDates(alternativeDates);
                }
            }
            else
            {
                ShowError("Izabrani smeštaj nije dostupan za odabrane datume i nema dostupnih alternativnih datuma.");
            }
        }

        private bool UserAcceptsAlternativeDates(List<DateTime> alternativeDates)
        {
            MessageBoxResult result = ShowAlternativeDatesMessage(alternativeDates);
            return result == MessageBoxResult.Yes;
        }

        private MessageBoxResult ShowAlternativeDatesMessage(List<DateTime> alternativeDates)
        {
            string alternativeDatesString = string.Join(", ", alternativeDates.Select(d => d.ToShortDateString()));
            return MessageBox.Show($"Nema dostupnih datuma za unete parametre. Predloženi alternativni datumi su: {alternativeDatesString}. Želite li odabrati jedan od ovih datuma?", "Predloženi alternativni datumi", MessageBoxButton.YesNo, MessageBoxImage.Information);
        }

        private void ReserveSelectedDates(List<DateTime> availableDatesList)
        {
            DateSelectionView dateSelectionView = new DateSelectionView(availableDatesList, NumberOfDays.Value);
            if (dateSelectionView.ShowDialog() == true)
            {
                DateTime selectedStartDate = dateSelectionView.GetSelectedStartDate();
                DateTime selectedEndDate = dateSelectionView.GetSelectedEndDate();
                Reservation newReservation = CreateReservation(selectedStartDate, selectedEndDate);
                _reservationService.Save(newReservation);
                _selectedAccommodation.Reservations.Add(newReservation);

                var superGuest = _superGuestService.GetByGuestId(User.Id);
                superGuest.AddReservation();
                superGuest.UseBonusPoint();
                _superGuestService.Update(superGuest);

                MessageBox.Show($"Smeštaj rezervisan za {NumberOfGuests.Value} gostiju od {selectedStartDate.ToShortDateString()} do {selectedEndDate.ToShortDateString()}.", "Uspešna rezervacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Rezervacija otkazana.", "Rezervacija otkazana", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private Reservation CreateReservation(DateTime startDate, DateTime endDate)
        {
            return new Reservation
            {
                Guest = User,
                Accommodation = _selectedAccommodation,
                ReservedDate = Tuple.Create(startDate, endDate),
                NumberOfGuests = NumberOfGuests.Value
            };
        }

        private bool ValidateInput() =>
            _selectedAccommodation != null && IsValidDate(StartDate, EndDate) && IsValidNumberOfGuests() && IsValidNumberOfDays()
                ? true : false;

        private bool IsValidDate(DateTime startDate, DateTime endDate) =>
            startDate >= DateTime.Now && endDate >= startDate
                ? true
                : ShowError("Unesite validan datum početka i završetka.", false);

        private bool IsValidNumberOfGuests() =>
            NumberOfGuests.HasValue && NumberOfGuests.Value >= 1 && NumberOfGuests.Value <= _selectedAccommodation.MaxGuests
                ? true
                : ShowError($"Unesite validan broj gostiju (između 1 i {_selectedAccommodation.MaxGuests}).", false);

        private bool IsValidNumberOfDays() =>
            NumberOfDays.HasValue && NumberOfDays.Value >= _selectedAccommodation.MinReservationDays && (EndDate - StartDate).TotalDays + 1 >= NumberOfDays.Value
                ? true
                : ShowError("Unesite validan broj dana (premašuje dostupan opseg).", false);

        private bool ShowError(string message, bool returnValue = true)
        {
            MessageBox.Show(message, "Greška pri rezervaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            return returnValue;
        }
    }
}