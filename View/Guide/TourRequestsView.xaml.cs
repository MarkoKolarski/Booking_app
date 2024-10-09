using System.Windows;
using System.Windows.Controls;
using BookingApp.ViewModel.Guide;
using BookingApp.Model;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Window
    {
        private TourRequestsViewModel tourRequestsViewModel;

        public TourRequestsView(int userId)
        {
            InitializeComponent();
            tourRequestsViewModel = new TourRequestsViewModel(userId);
            DataContext = tourRequestsViewModel;
        }

        public void btnAcceptTourRequest_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsViewModel.HandleTourRequest((TourRequest)((Button)sender).CommandParameter, TourRequestStatus.Accepted);
        }

        public void btnRejectTourRequest_Click(object sender, RoutedEventArgs e)
        {
            tourRequestsViewModel.HandleTourRequest((TourRequest)((Button)sender).CommandParameter, TourRequestStatus.Invalid);
        }
    }
}
