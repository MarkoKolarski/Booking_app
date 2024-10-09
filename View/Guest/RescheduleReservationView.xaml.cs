using BookingApp.Model;
using BookingApp.ViewModel.Guest;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.Guest
{

    public partial class RescheduleReservationView : Window
    {
        private readonly RescheduleReservationViewModel _viewModel;
        private readonly User user;
        public RescheduleReservationView(Reservation selectedReservation, User user)
        {
            this.user = user;
            InitializeComponent();
            _viewModel = new RescheduleReservationViewModel(user, selectedReservation); ;
            DataContext = _viewModel;
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousImage();
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NextImage();
        }

        private void RescheduleReservation(object sender, RoutedEventArgs e)
        {
           if( _viewModel.RescheduleReservation())
            {
                titleBar.OpenManageReservationsView(sender, e);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            titleBar.OpenManageReservationsView(sender, e);
        }

    }
}
