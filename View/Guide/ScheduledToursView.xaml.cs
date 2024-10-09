using System.Windows;
using System.Windows.Controls;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for ScheduledToursView.xaml
    /// </summary>
    public partial class ScheduledToursView : Window
    {
        private ScheduledToursViewModel scheduledToursViewModel;

        public ScheduledToursView(int userId)
        {
            InitializeComponent();
            scheduledToursViewModel = new ScheduledToursViewModel(userId);
            DataContext = scheduledToursViewModel;
        }

        private void btnStartTour_Click(object sender, RoutedEventArgs e)
        {
            scheduledToursViewModel.StartTour((TourDto)((Button)sender).CommandParameter);
        }

        private void btnCancelTour_Click(object sender, RoutedEventArgs e)
        {
            scheduledToursViewModel.CancelTour((TourDto)((Button)sender).CommandParameter);
        }
    }
}
