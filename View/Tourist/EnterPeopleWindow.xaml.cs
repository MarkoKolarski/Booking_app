using System.Windows;
using System.Windows.Controls;
using BookingApp.Model;
using BookingApp.Dto;
using BookingApp.ViewModel.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class EnterPeopleWindow : Window
    {
        private EnterPeopleViewModel _viewModel;
        private readonly User user;
        public EnterPeopleWindow(User user, ScheduledTour tour)
        {
            InitializeComponent();
            this.user = user;
            _viewModel = new EnterPeopleViewModel(user, tour);
            DataContext = _viewModel;
        }

        private void CheckAvailability_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(_viewModel.NumberOfPeople, out int numberOfPeople))
            {
                if (_viewModel.TourIsFullyBooked())
                {
                    ManageFullyBookedTour();
                }
                else if (numberOfPeople <= _viewModel.AvailableSpots())
                {
                    ManageAvailableSpots();
                }
                else
                {
                    ManageNotEnoughSpots();
                }
            }
            else
            {
                MessageBox.Show("Please, enter valid number of persons.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                HideReservationControls();
            }
        }

        private void ManageFullyBookedTour()
        {
            MessageBox.Show("Tuor is fully booked.", "Availability", MessageBoxButton.OK, MessageBoxImage.Warning);
            lstAlternativeTours.ItemsSource = _viewModel.ShowOtherToursOnSameLocation();
            lstAlternativeTours.Visibility = Visibility.Visible;
            btnCancelReservation.Visibility = Visibility.Visible;
            HideReservationControls();
        }

        private void ManageAvailableSpots()
        {
            MessageBox.Show("Spots are available on this tour.", "Availability", MessageBoxButton.OK, MessageBoxImage.Information);
            ShowReservationControls();
            btnCancelReservation.Visibility = Visibility.Visible;
        }

        private void ManageNotEnoughSpots()
        {
            int remainingSpots = _viewModel.AvailableSpots();
            MessageBox.Show($"Not enough spots on this tour. (Number of free spots: {remainingSpots})", "Availability", MessageBoxButton.OK, MessageBoxImage.Warning);
            btnCancelReservation.Visibility = Visibility.Visible;
        }

        private void ShowReservationControls()
        {
            personInputs.Visibility = Visibility.Visible;
            addParticipantButton.Visibility = Visibility.Visible;
            btnMakeReservation.Visibility = Visibility.Collapsed;
            btnMakeVoucherReservation.Visibility = Visibility.Collapsed;
        }

        private void HideReservationControls()
        {
            personInputs.Visibility = Visibility.Collapsed;
            addParticipantButton.Visibility = Visibility.Collapsed;
            btnMakeReservation.Visibility = Visibility.Collapsed;
            btnMakeVoucherReservation.Visibility = Visibility.Collapsed;
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MakeReservation();
            Close();
        }

        private void MakeVoucherReservation_Click(object sender, RoutedEventArgs e)
        {
            VoucherView voucherView = new VoucherView(user, _viewModel.Participants, _viewModel);
            voucherView.Show();
            Close();
        }

        private void AddParticipant_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddParticipant();
            UpdateButtonVisibility();
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
        }

        private void UpdateButtonVisibility()
        {
            if (_viewModel._enteredParticipantsCount == int.Parse(_viewModel.NumberOfPeople))
            {
                btnMakeReservation.Visibility = Visibility.Visible;
                btnMakeVoucherReservation.Visibility = Visibility.Visible;
                addParticipantButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                addParticipantButton.Visibility = Visibility.Visible;
                btnMakeReservation.Visibility = Visibility.Collapsed;
                btnMakeVoucherReservation.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelReservation();
            Close();
        }

        private void ReserveAlternativeTour_Click(object sender, RoutedEventArgs e)
        {
            if (lstAlternativeTours.SelectedItem != null)
            {
                TourDto selectedTour = (TourDto)lstAlternativeTours.SelectedItem;
                ScheduledTour scheduledTour = selectedTour.ScheduledTour;
                EnterPeopleWindow enterPeople = new EnterPeopleWindow(user, scheduledTour);
                enterPeople.Show();
                Close();
            }
        }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = string.Empty;
        }
    }
}