using BookingApp.Model;
using BookingApp.ViewModel.Guest;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.Guest
{
    public partial class ManageReservationsView : Window
    {
        private readonly ManageReservationsViewModel _viewModel;
        private readonly User user;

        public ManageReservationsView(User user)
        {
            this.user = user;
            InitializeComponent();
            _viewModel = new ManageReservationsViewModel(user); ;
            DataContext = _viewModel;
            LoadReservations();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Reservation? selectedReservation = OldReservationsListView.SelectedItem as Reservation;
            if (_viewModel.RateAccommodation())
            {
                titleBar.OpenRateAccommodationView(sender, e, selectedReservation);
            }   
        }
        private void RescheduleReservation_Click(object sender, RoutedEventArgs e)
        {
            Reservation? selectedReservation = ReservationsListView.SelectedItem as Reservation;
            if (_viewModel.RescheduleReservation())
            {
                titleBar.OpenRescheduleReservationView(sender, e, selectedReservation);
            }
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelReservation();
        }
        //OpenReservationInformationView
        private void OpenReservationInformationView_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenReservationInformationView();
        }

        private void LoadReservations()
        {
            ReservationsListView.ItemsSource = _viewModel.CurrentReservations;
            OldReservationsListView.ItemsSource = _viewModel.ExpiredReservations;
        }


    }

}