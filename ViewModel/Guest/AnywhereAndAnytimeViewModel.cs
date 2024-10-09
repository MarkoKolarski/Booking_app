using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.Utils;
using BookingApp.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class AnywhereAndAnytimeViewModel : ViewModelBase
    {
        private readonly AccommodationService _service;
        private readonly ReservationService _reservationService;
        public List<Accommodation> AllAccommodations { get; set; }

        private List<Accommodation> _filteredAccommodations;
        public List<Accommodation> FilteredAccommodations
        {
            get { return _filteredAccommodations; }
            set
            {
                _filteredAccommodations = value;
                OnPropertyChanged(nameof(FilteredAccommodations));
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

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

            }
        }

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

        public User User { get; set; }

        public ObservableCollection<AccommodationSearchDto> AccommodationSearchDtos { get; set; }

        public ICommand SearchCommand { get; }
        public ICommand CancelSearchCommand { get; }
        public ICommand ReserveAccommodationCommand { get; }
        public ICommand FocusCommand { get; private set; }

        public AnywhereAndAnytimeViewModel(User user)
        {
            User = user;
            _service = new AccommodationService();
            _reservationService = new ReservationService();
            AllAccommodations = _service.GetAll();
            FilteredAccommodations = new List<Accommodation>(AllAccommodations);
            AccommodationSearchDtos = new ObservableCollection<AccommodationSearchDto>();

            SearchCommand = new RelayCommand(_ => SearchAccommodation());
            CancelSearchCommand = new RelayCommand(_ => ResetSearch());
            ReserveAccommodationCommand = new RelayCommand(_ => ReserveAccommodation());
            FocusCommand = new RelayCommand(ExecuteFocusCommand);

            PopulateAccommodationSearchDtos(AllAccommodations);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public void SearchAccommodation()
        {
            if (!ValidateInput()) return;

            Parallel.ForEach(AllAccommodations, accommodation =>
            {
                accommodation.Reservations = _reservationService.GetReservationsByAccommodationId(accommodation.Id) ?? new List<Reservation>();
            });

            FilteredAccommodations = FilterAccommodations();

            if (FilteredAccommodations.Count == 0)
            {
                return;
            }
            else
            {
                PopulateAccommodationSearchDtos(FilteredAccommodations);
            }
        }

        private bool ValidateInput() =>
            IsValidDate(StartDate, EndDate, NumberOfDays) && IsValidNumberOfDays()
                ? true : false;
        private bool IsValidDate(DateTime? startDate, DateTime? endDate, int? numberOfDays)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return true;
            }

            if (!AreDatesValid(startDate.Value, endDate.Value))
            {
                ShowError("Unesite validan datum početka i završetka.");
                return false;
            }

            if (!IsValidNumberOfDays(numberOfDays))
            {
                ShowError("Morate uneti broj dana.");
                return false;
            }

            if (!IsNumberOfDaysValid(numberOfDays.Value))
            {
                ShowError("Unesite validan broj dana (broj dana mora biti veći od nule).");
                return false;
            }

            if (!AreDatesInFuture(startDate.Value, endDate.Value))
            {
                ShowError("Datum ne sme biti u prošlosti.");
                return false;
            }

            return true;
        }

        private bool AreDatesValid(DateTime startDate, DateTime endDate)
        {
            return startDate <= endDate;
        }

        private bool IsValidNumberOfDays(int? numberOfDays)
        {
            return numberOfDays.HasValue;
        }

        private bool IsNumberOfDaysValid(int numberOfDays)
        {
            return numberOfDays > 0;
        }

        private bool AreDatesInFuture(DateTime startDate, DateTime endDate)
        {
            return startDate >= DateTime.Today && endDate >= DateTime.Today;
        }




        private bool IsValidNumberOfDays()
        {
            if (NumberOfDays.HasValue && StartDate.HasValue && EndDate.HasValue)
            {

                TimeSpan difference = EndDate.Value - StartDate.Value;

                if (difference.TotalDays + 1 >= NumberOfDays.Value)
                {
                    return true;
                }
                else
                {
                    ShowError("Unesite validan broj dana (premašuje dostupan opseg).");
                    return false;
                }
            }
            return true;
        }

        private bool ShowError(string message, bool returnValue = true)
        {
            MessageBox.Show(message, "Greška pri pretrazi", MessageBoxButton.OK, MessageBoxImage.Error);
            return returnValue;
        }

        private List<Accommodation> FilterAccommodations()
        {
            var accommodations = AllAccommodations.AsParallel();

            if (NumberOfGuests.HasValue)
            {
                accommodations = accommodations.Where(a => a.MaxGuests >= NumberOfGuests.Value);
            }

            if (NumberOfDays.HasValue)
            {
                accommodations = accommodations.Where(a => ReservationUtils.GetAvailableDates(a, DateTime.Now, DateTime.Now.AddMonths(2), NumberOfDays).Any());
            }

            if (StartDate.HasValue && EndDate.HasValue)
            {
                accommodations = accommodations.Where(a => ReservationUtils.GetAvailableDates(a, StartDate.Value, EndDate.Value, NumberOfDays).Any());
            }

            return accommodations.ToList();
        }

        private void PopulateAccommodationSearchDtos(List<Accommodation> accommodations)
        {
            AccommodationSearchDtos.Clear();
            foreach (var accommodation in accommodations)
            {
                AccommodationSearchDtos.Add(new AccommodationSearchDto(accommodation));
            }
        }

        public void ResetSearch()
        {
            NumberOfGuests = null;
            NumberOfDays = null;
            StartDate = null;
            EndDate = null;
            FilteredAccommodations = new List<Accommodation>(AllAccommodations);
            PopulateAccommodationSearchDtos(AllAccommodations);
        }

        private void ReserveAccommodation()
        {
            if (SelectedAccommodation != null)
            {

                var reserveAccommodationView = new ReserveAccommodationView(User, SelectedAccommodation);
                CloseWindow();
                reserveAccommodationView.Show();
            }
            else
            {
                ShowError("Morate selektovati forum da biste videli komentare.");
            }
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }

    }

}
