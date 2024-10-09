using System.Windows;
using BookingApp.Model;
using BookingApp.Dto;
using BookingApp.ViewModel.Tourist;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace BookingApp.View.Tourist
{
    public partial class TourWindow : Window
    {
        private TourViewModel tourViewModel;
        private readonly User user;

        public TourWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            tourViewModel = new TourViewModel(user);
            DataContext = tourViewModel;
        }

        private void LocationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
            bool isValidInput = !regex.IsMatch(e.Text);
            LocationErrorText.Visibility = isValidInput ? Visibility.Collapsed : Visibility.Visible;
            e.Handled = !isValidInput;
        }
        private void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            bool isValidInput = !regex.IsMatch(e.Text);
            DurationErrorText.Visibility = isValidInput ? Visibility.Collapsed : Visibility.Visible;
            e.Handled = !isValidInput;
        }

        private void LanguageTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
            bool isValidInput = !regex.IsMatch(e.Text);
            LanguageErrorText.Visibility = isValidInput ? Visibility.Collapsed : Visibility.Visible;
            e.Handled = !isValidInput;
        }

        private void GroupSizeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            bool isValidInput = !regex.IsMatch(e.Text);
            GroupSizeErrorText.Visibility = isValidInput ? Visibility.Collapsed : Visibility.Visible;
            e.Handled = !isValidInput;
        }

        private void lstTours_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (lstTours.SelectedItem != null)
            {
                TourDto selectedTour = (TourDto)lstTours.SelectedItem;
                ScheduledTour scheduledTour = selectedTour.ScheduledTour;
                EnterPeopleWindow enterPeopleWindow = new EnterPeopleWindow(user, scheduledTour);
                enterPeopleWindow.ShowDialog();
            }
        }

        private void OpenFollowJoinPage_Click(object sender, RoutedEventArgs e)
        {
            FollowJoinTourView followJoinTourView = new FollowJoinTourView(user);
            followJoinTourView.Show();
        }

        private void OpenVouchersPage_Click(object sender, RoutedEventArgs e)
        {
            VoucherView voucherView = new VoucherView(user);
            voucherView.Show();
        }

        private void OpenTourReviewPage_Click(object sender, RoutedEventArgs e)
        {
            TourRatingView tourRatingView = new TourRatingView(user);
            tourRatingView.Show();
        }

        private void OpenCreateTourRequestPage_Click(object sender, RoutedEventArgs e)
        {
            CreateTourRequestView createTourRequestView = new CreateTourRequestView(user);
            createTourRequestView.Show();
        }

        private void OpenRequestStatisticsPage_Click(object sender, RoutedEventArgs e)
        {
            RequestStatisticsView requestStatisticsView = new RequestStatisticsView(user);
            requestStatisticsView.Show();
        }

        private void OpenNotificationsPage_Click(object sender, RoutedEventArgs e)
        {
            NotificationView notificationView = new NotificationView(user);
            notificationView.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
