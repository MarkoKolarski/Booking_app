using System.Windows;
using BookingApp.Dto;
using System.Windows.Controls;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for FinishedToursView.xaml
    /// </summary>
    public partial class FinishedToursView : Window
    {
        private FinishedToursViewModel finishedToursViewModel;

        public FinishedToursView(int userId)
        {
            InitializeComponent();
            finishedToursViewModel = new FinishedToursViewModel(userId);
            DataContext = finishedToursViewModel;
        }

        private void cbMostVisitedTour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            finishedToursViewModel.MostVisitedTour();
        }

        private void btnTourStatistics_Click(object sender, RoutedEventArgs e)
        {
            TourStatisticsView tourStatisticsView = new TourStatisticsView((TourDto)((Button)sender).CommandParameter);
            tourStatisticsView.ShowDialog();
        }

        private void btnTourReviews_Click(object sender, RoutedEventArgs e)
        {
            TourReviewsView tourReviewsView = new TourReviewsView((TourDto)((Button)sender).CommandParameter);
            tourReviewsView.ShowDialog();
        }
    }
}
