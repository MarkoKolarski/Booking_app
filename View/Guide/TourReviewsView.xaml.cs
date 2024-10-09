using System.Windows;
using System.Windows.Controls;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourReviewsView.xaml
    /// </summary>
    public partial class TourReviewsView : Window
    {
        TourReviewsViewModel tourReviewsViewModel;

        public TourReviewsView(TourDto tourDto)
        {
            InitializeComponent();
            tourReviewsViewModel = new TourReviewsViewModel(tourDto);
            DataContext = tourReviewsViewModel;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        { 
            tourReviewsViewModel.ReportReview((TourReview)((Button)sender).CommandParameter);
        }
    }
}
