using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Service;

namespace BookingApp.ViewModel.Owner
{
    public class LastCheckoutsViewModel : ViewModelBase
    {
        public User currentUser { get; set; }

        private readonly ReservationService _reservationService;

        private readonly GuestRatingService _guestRatingService;
        public ObservableCollection<Reservation> Reservations { get; set; }

        public List<Reservation> SelectedReservations { get; set; }
        public LastCheckoutsViewModel(User user)
        {
            currentUser = user;
            _reservationService = new ReservationService();
            _guestRatingService = new GuestRatingService();
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetLastCheckouts(currentUser.Id, DateTime.Now));
            SelectedReservations = _reservationService.GetAll();
        }

        public void ShowGuestRatingWindow(object sender, MouseButtonEventArgs e, DataGrid checkout)
        {
            Reservation selectedReservation = (Reservation)checkout.SelectedItem;

            if (!_reservationService.CheckIfCanBeRated(selectedReservation))
            {
                MessageBox.Show("Checkout is older than 5 days, the user cannot be rated!!!"); return;
            }

            if (_guestRatingService.ExistsByReservationId(selectedReservation.Id))
            {
                MessageBox.Show("Checkot was already Rated");
                return;
            }

            if (selectedReservation != null)
            {
                GuestRatingView guestRatingView = new GuestRatingView(selectedReservation.Id, currentUser.Id);
                Application.Current.MainWindow.Content = guestRatingView;
            }
        }
    }
}
