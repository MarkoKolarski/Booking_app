using BookingApp.Model;
using BookingApp.View;
using BookingApp.View.Guest;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.UserControls
{
    public partial class CustomTitleBar : UserControl
    {
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register(
            "User", typeof(User), typeof(CustomTitleBar), new PropertyMetadata(null));

        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public CustomTitleBar()
        {
            InitializeComponent();
        }

        public CustomTitleBar(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window window = Window.GetWindow(this);
                if (window != null)
                    window.DragMove();
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void HandleKeyShortcut(Key key, Action action, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (e.Key == key)
                {
                    action();
                    e.Handled = true;
                }
            }
        }

        private void HandleKeyShortcut2(Key key, Action action, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Alt ) == ModifierKeys.Alt)
            {
                if (e.Key == key)
                {
                    action();
                    e.Handled = true;
                }
            }
        }

        public void HandlePreviewKeyDown(KeyEventArgs e)
        {
            HandleKeyShortcut(Key.D2, () => ShowAccommodationOverview(null, null), e);
            HandleKeyShortcut(Key.D1, () => OpenGuestMainWindow(null, null), e);
            HandleKeyShortcut(Key.D3, () => OpenOwnerRatingsView(null, null), e);
            HandleKeyShortcut(Key.D4, () => OpenManageReservationsView(null, null), e);
            HandleKeyShortcut(Key.D5, () => OpenAnywhereAndAnytimeView(null, null), e);
            HandleKeyShortcut(Key.D6, () => OpenManageForumsView(null, null), e);
            HandleKeyShortcut(Key.D7, () => OpenTutorialView(null, null), e);
            HandleKeyShortcut(Key.D8, () => OpenShortcutsView(null, null), e);
            HandleKeyShortcut(Key.D9, () => OpenRescheduleRequestNotificationView(null, null), e);
            HandleKeyShortcut(Key.D0, () => OpenSignInForm(null, null), e);

            HandleKeyShortcut2(Key.D1, () => CloseWindow(this, new RoutedEventArgs()), e);
            HandleKeyShortcut2(Key.D2, () => MinimizeWindow(this, new RoutedEventArgs()), e);

            base.OnPreviewKeyDown(e);
        }

        private async void OpenView<T>(Type viewType, object param1 = null, object param2 = null, object sender = null, RoutedEventArgs e = null)
        {
            Window existingView = CheckIfViewIsAlreadyOpen(viewType);
            if (existingView != null)
            {
                // If it's already open, bring it to the front
                existingView.Activate();
                return;
            }

            if (param1 != null && param2 != null)
            {
                await OpenViewWithTwoParameters(viewType, param1, param2);
            }
            else if (param1 != null)
            {
                await OpenViewWithOneParameter(viewType, param1);
            }
            else
            {
                await OpenViewWithNoParameters(viewType);
            }

            await CloseCurrentWindow();
        }

        private Window CheckIfViewIsAlreadyOpen(Type viewType)
        {
            return Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w != null && w.GetType() == viewType);
        }

        private async Task OpenViewWithTwoParameters(Type viewType, object param1, object param2)
        {
            if (Activator.CreateInstance(viewType, param1, param2) is Window newViewInstance)
            {

                newViewInstance.Show();


                await newViewInstance.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Delay(100); 
                }, System.Windows.Threading.DispatcherPriority.ContextIdle);
            }
        }

        private async Task OpenViewWithNoParameters(Type viewType)
        {
            if (Activator.CreateInstance(viewType) is Window newViewInstance)
            {

                newViewInstance.Show();

                // Wait for the new window to open
                await newViewInstance.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Delay(100);
                }, System.Windows.Threading.DispatcherPriority.ContextIdle);
            }
        }

        private async Task OpenViewWithOneParameter(Type viewType, object param1)
        {
            if (Activator.CreateInstance(viewType, param1) is Window newViewInstance)
            {

                newViewInstance.Show();

                // Wait for the new window to open
                await newViewInstance.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Delay(100); 
                }, System.Windows.Threading.DispatcherPriority.ContextIdle);
            }
        }

        public void ShowAccommodationOverview(object sender, RoutedEventArgs e)
        {
            OpenView<SearchAccommodationView>(typeof(SearchAccommodationView), User, null, sender, e);
        }

        public void OpenRescheduleRequestNotificationView(object sender, RoutedEventArgs e)
        {
            OpenView<RescheduleRequestNotificationView>(typeof(RescheduleRequestNotificationView), User, null, sender, e);
        }

        public void OpenManageReservationsView(object sender, RoutedEventArgs e)
        {
            OpenView<ManageReservationsView>(typeof(ManageReservationsView), User, null, sender, e);
        }

        public void OpenGuestMainWindow(object sender, RoutedEventArgs e)
        {
            OpenView<GuestMainWindow>(typeof(GuestMainWindow), User, null, sender, e);
        }

        public void OpenReserveAccommodationView(object sender, RoutedEventArgs e, Accommodation selectedAccommodation)
        {
            OpenView<ReserveAccommodationView>(typeof(ReserveAccommodationView), User, selectedAccommodation, sender, e);
        }

        public void OpenRateAccommodationView(object sender, RoutedEventArgs e, Reservation selectedReservation)
        {
            OpenView<RateAccommodationView>(typeof(RateAccommodationView), User, selectedReservation, sender, e);
        }


        public void OpenRescheduleReservationView(object sender, RoutedEventArgs e, Reservation selectedReservation)
        {
            OpenView<RescheduleReservationView>(typeof(RescheduleReservationView), selectedReservation,  User, sender, e);
        }

        public void OpenOwnerRatingsView(object sender, RoutedEventArgs e)
        {
            OpenView<OwnerRatingsView>(typeof(OwnerRatingsView), User, null, sender, e);
        }

        public void OpenAnywhereAndAnytimeView(object sender, RoutedEventArgs e)
        {
            OpenView<AnywhereAndAnytimeView>(typeof(AnywhereAndAnytimeView), User, null, sender, e);
        }

        public void OpenManageForumsView(object sender, RoutedEventArgs e)
        {
            OpenView<ManageForumsView>(typeof(ManageForumsView), User, null, sender, e);
        }

        public void OpenSignInForm(object sender, RoutedEventArgs e)
        {
            OpenView<SignInForm>(typeof(SignInForm), null, null, sender, e);
        }

        public void OpenShortcutsView(object sender, RoutedEventArgs e)
        {
            OpenView<ShortcutsView>(typeof(ShortcutsView), User, null, sender, e);
        }

        public void OpenTutorialView(object sender, RoutedEventArgs e)
        {
            OpenView<TutorialView>(typeof(TutorialView), User, null, sender, e);
        }

        private async void CloseWindow(object sender, RoutedEventArgs e)
        {
            await CloseCurrentWindow();
        }

        private async Task CloseCurrentWindow()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Window window = Window.GetWindow(this);
                if (window != null)
                    window.Close();
            }, System.Windows.Threading.DispatcherPriority.ContextIdle);
        }

    }
}