using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;
using BookingApp.View.Guest;

namespace BookingApp.ViewModel.Guest
{
    public class ManageReservationsViewModel : ViewModelBase
    {
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        private Reservation _selectedReservation;
        public ObservableCollection<Reservation> CurrentReservations { get; set; }
        public ObservableCollection<Reservation> ExpiredReservations { get; set; }
        public Reservation SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                _oldSelectedReservation = null;
                OnPropertyChanged(nameof(SelectedReservation));
                OnPropertyChanged(nameof(OldSelectedReservation));

            }
        }

        private Reservation _oldSelectedReservation;
        public Reservation OldSelectedReservation
        {
            get { return _oldSelectedReservation; }
            set
            {
                _oldSelectedReservation = value;
                _selectedReservation = null;
                OnPropertyChanged(nameof(OldSelectedReservation));
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public User User { get; set; }
        public ICommand FocusCommand { get; private set; }

        public ManageReservationsViewModel(User user)
        {
            User = user;
            _accommodationService = new AccommodationService();
            _reservationService = new ReservationService();
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            LoadReservations();

            FocusCommand = new RelayCommand(ExecuteFocusCommand);

        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public void CancelReservation()
        {
            if (SelectedReservation == null && OldSelectedReservation == null)
            {
                ShowErrorMessage("Izaberite tekuću rezervaciju.");
                return;
            }

            if (SelectedReservation == null)
            {
                ShowErrorMessage("Ne možete otkazati arhiviranu rezervaciju. Možete otkazati samo tekuće rezervacije.");
                return;
            }

            if (!CanCancelReservation(SelectedReservation))
            {
                return;
            }

            //_reservationService.Delete(SelectedReservation);
            SelectedReservation.Deleted = true;
            _reservationService.Update(SelectedReservation);
            LoadReservations();
        }

        private bool CanCancelReservation(Reservation reservation)
        {

            if (reservation.ReservedDate.Item1.AddDays(-1) < DateTime.Now)
            {
                ShowErrorMessage("Možete otkazati rezervaciju najkasnije 24 sata pre početka boravka.");
                return false;
            }

            if (reservation.Accommodation.CancellationDays > 0 && reservation.ReservedDate.Item1.AddDays(-reservation.Accommodation.CancellationDays) < DateTime.Now)
            {
                ShowErrorMessage($"Ovu rezervaciju možete otkazati najkasnije {reservation.Accommodation.CancellationDays} dana pre početka boravka.");
                return false;
            }

            return true;
        }

        private void LoadReservations()
        {
            var guestReservations = _reservationService.GetByGuestId(User.Id);
            var currentDateTime = DateTime.Now;
            LoadCurrentReservations(guestReservations, currentDateTime);
            LoadExpiredReservations(guestReservations, currentDateTime);
        }

        private void LoadCurrentReservations(IEnumerable<Reservation> guestReservations, DateTime currentDateTime)
        {
            var currentReservations = guestReservations.Where(r => r.ReservedDate.Item2 >= currentDateTime);
            currentReservations = currentReservations.Where(a => a.Deleted == false).ToList();
            if (CurrentReservations == null)
            {
                CurrentReservations = new ObservableCollection<Reservation>(currentReservations);
            }
            else
            {
                UpdateExistingCollection(CurrentReservations, currentReservations);
            }
        }

        private void LoadExpiredReservations(IEnumerable<Reservation> guestReservations, DateTime currentDateTime)
        {
            var expiredReservations = guestReservations.Where(r => r.ReservedDate.Item2 < currentDateTime);
            //expiredReservations = expiredReservations.Where(a => a.Deleted == false).ToList();

            if (ExpiredReservations == null)
            {
                ExpiredReservations = new ObservableCollection<Reservation>(expiredReservations);
            }
            else
            {
                UpdateExistingCollection(ExpiredReservations, expiredReservations);
            }
        }

        private void UpdateExistingCollection<T>(ObservableCollection<T> existingCollection, IEnumerable<T> newItems)
        {

            existingCollection.Clear();

            foreach (var item in newItems)
            {
                existingCollection.Add(item);
            }
        }

        public bool RateAccommodation()
        {

            if (!CanRateAccommodation())
            {
                return false;
            }

            //RateAccommodationView rateAccommodationView = new RateAccommodationView(User, OldSelectedReservation);
            //CloseWindow();
            //rateAccommodationView.ShowDialog();
            return true;
        }
        private bool CanRateAccommodation()
        {
            if (!CheckOldSelectedReservation())
            {
                return false;
            }

            if (!IsWithinFiveDaysOfStay())
            {
                ShowErrorMessage("Možete ostaviti ocenu samo u roku od 5 dana nakon vašeg boravka.");
                return false;
            }

            if (IsAlreadyRated())
            {
                ShowErrorMessage("Već ste ocenili ovu rezervaciju.");
                return false;
            }

            return true;
        }

        private bool IsWithinFiveDaysOfStay()
        {
            if (OldSelectedReservation == null)
            {
                ShowErrorMessage("Nemate nijednu rezervaciju za ovaj smeštaj.");
                return false;
            }

            DateTime lastReservedEndDate = OldSelectedReservation.ReservedDate.Item2;
            TimeSpan timeSinceStay = DateTime.Now - lastReservedEndDate;

            if (timeSinceStay.TotalDays <= 5)
            {
                return true;
            }

            return false;
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

        private bool IsAlreadyRated()
        {
            return OldSelectedReservation.AccommodationRating != null;
        }

        private bool CheckOldSelectedReservation()
        {
            if (SelectedReservation == null && OldSelectedReservation == null)
            {
                ShowErrorMessage("Izaberite arhiviranu rezervaciju.");
                return false;
            }

            if (OldSelectedReservation == null)
            {
                ShowErrorMessage("Ne možete oceniti tekuću rezervaciju. Možete oceniti samo istekle rezervacije.");
                return false;
            }

            return true;
        }
        public bool RescheduleReservation()
        {
            if (SelectedReservation == null)
            {
                ShowErrorMessage("Morate izabrati tekuću rezervaciju.");
                return false;
            }

            if (IsReservationRescheduled())
            {
                ShowErrorMessage("Možete poslati samo jedan zahtev za pomeranje!");
                return false;
            }

            if (IsReservationOngoing())
            {
                ShowErrorMessage("Ne možete pomeriti tekuću rezervaciju koja je u toku!");
                return false;
            }

            return true;
        }

        private bool IsReservationOngoing()
        {
            DateTime now = DateTime.Now;
            return now >= SelectedReservation.ReservedDate.Item1 && now <= SelectedReservation.ReservedDate.Item2;
        }

        public bool IsReservationRescheduled()
        {
            return _rescheduleReservationRequestService.GetAll().Any(r => r.Reservation.Id == SelectedReservation.Id);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void OpenReservationInformationView()
        {
            if (SelectedReservation == null && OldSelectedReservation == null)
            {
                ShowErrorMessage("Morate izabrati rezervaciju.");
                return;
            }

            if (SelectedReservation == null)
            {
                ReservationInformationView reservationInformationView = new ReservationInformationView(User, OldSelectedReservation);
                CloseWindow();
                reservationInformationView.ShowDialog();
            }
            else
            {
                ReservationInformationView reservationInformationView = new ReservationInformationView(User, SelectedReservation);
                CloseWindow();
                reservationInformationView.ShowDialog();
            }
        }

    }
}
