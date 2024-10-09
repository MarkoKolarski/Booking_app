using BookingApp.Model;
using BookingApp.UserControls;
using BookingApp.ViewModel.Guest;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.Guest
{
    public partial class GuestMainWindow : Window
    {
        private readonly User user;
        private readonly GuestMainWindowModel _viewModel;
        private readonly RescheduleRequestNotificationViewModel _rescheduleRequestNotificationViewModel;

        public GuestMainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            _viewModel = new GuestMainWindowModel(user);
            DataContext = _viewModel;
            _rescheduleRequestNotificationViewModel = new RescheduleRequestNotificationViewModel(user);
            Loaded += GuestMainWindow_Loaded;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void GuestMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int unreadNotifications = _rescheduleRequestNotificationViewModel.UnreadNotifications;
            if (unreadNotifications > 0)
            {
                string message = "Imate " + unreadNotifications + " ne pročitano/a obaveštenja.";
                MessageBox.Show(message, "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowAccommodationOverview(object sender, RoutedEventArgs e)
        {
            titleBar.ShowAccommodationOverview(sender, e);
        }

        private void OpenAnywhereAndAnytimeView(object sender, RoutedEventArgs e)
        {
            titleBar.OpenAnywhereAndAnytimeView(sender, e);
        }

        private void OpenManageReservationsView(object sender, RoutedEventArgs e)
        {
            titleBar.OpenManageReservationsView(sender, e);
        }
        private void OpenManageForumsView(object sender, RoutedEventArgs e)
        {
            titleBar.OpenManageForumsView(sender, e);
        }

        private void OpenOwnerRatingsView(object sender, RoutedEventArgs e)
        {
            titleBar.OpenOwnerRatingsView(sender, e);
        }

        private void OpenSuperGuestDescriptionView(object sender, RoutedEventArgs e)
        {
            SuperGuestDescriptionView superGuestDescription = new SuperGuestDescriptionView(user);
            superGuestDescription.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
