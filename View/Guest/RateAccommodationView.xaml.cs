using BookingApp.Model;
using BookingApp.Service;
using BookingApp.ViewModel.Guest;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.Guest
{
    public partial class RateAccommodationView : Window
    {
        private readonly RateAccommodationViewModel viewModel;
        private readonly User user;
        private readonly AccommodationService _accommodationService = new AccommodationService();

        public RateAccommodationView(User user, Reservation selectedReservation)
        {
            this.user = user;
            InitializeComponent();
            ReservationService _reservationService = new ReservationService();

            Accommodation accommodation = _accommodationService.FindById(selectedReservation.Accommodation.Id);
            accommodation.Reservations = _reservationService.GetReservationsByAccommodationId(accommodation.Id);

            viewModel = new RateAccommodationViewModel(accommodation, user, selectedReservation);
            DataContext = viewModel;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PreviousImage();
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NextImage();
        }

        private void SubmitRating_Click(object sender, RoutedEventArgs e)
        {

            if (viewModel.SubmitRating())
            {
                titleBar.OpenManageReservationsView(sender, e);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            titleBar.OpenManageReservationsView(sender , e);
        }

        private void BrowseImages_Click(object sender, RoutedEventArgs e)
        {
            viewModel.BrowseImages();
        }

        private void recommendationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}