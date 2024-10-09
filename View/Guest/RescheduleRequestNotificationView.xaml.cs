using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.ViewModel.Guest;

namespace BookingApp.View.Guest
{
    public partial class RescheduleRequestNotificationView : Window
    {
        private RescheduleRequestNotificationViewModel _viewModel;
        private readonly User user;
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;

        public RescheduleRequestNotificationView(User user)
        {
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            this.user = user;
            InitializeComponent();
            _viewModel = new RescheduleRequestNotificationViewModel(user);
            DataContext = _viewModel;
            //_viewModel.LoadRescheduleRequests();
            MarkAllRequestsAsRead();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.LeftButton == MouseButtonState.Pressed && this.WindowState == WindowState.Normal)
            {
                DragMove();
            }
        }

        private void OpenGuestMainWindow(object sender, RoutedEventArgs e)
        {
            GuestMainWindow guestMainWindow = new GuestMainWindow(user);
            guestMainWindow.Show();
            this.Close();
        }

        private void ShowAccommodationOverview(object sender, RoutedEventArgs e)
        {
            SearchAccommodationView searchAccommodationView = new SearchAccommodationView(user);
            searchAccommodationView.Show();
            this.Close();
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestoreWindow(object sender, RoutedEventArgs e)
        {

            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private readonly object _lockObject = new object();

        private void MarkAllRequestsAsRead()
        {
            Parallel.ForEach(_viewModel.RescheduleRequests, request =>
            {
                if (!request.IsRead)
                {
                    lock (_lockObject)
                    {
                        request.IsRead = true;
                        _rescheduleReservationRequestService.Update(request);
                    }
                }
            });
        }
    }
}
