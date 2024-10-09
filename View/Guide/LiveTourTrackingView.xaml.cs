using System.Windows;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    public partial class LiveTourTrackingView : Window
    {
        private LiveTourTrackingViewModel liveTourTrackingViewModel;

        public LiveTourTrackingView(TourDto tourDto)
        {
            InitializeComponent();
            liveTourTrackingViewModel = new LiveTourTrackingViewModel(tourDto);
            DataContext = liveTourTrackingViewModel;

            liveTourTrackingViewModel.IsLiveTourTrackingFinished += (sender, tourDto) =>
            {
                MarkTouristsView markTouristsView = new MarkTouristsView(tourDto);
                markTouristsView.ShowDialog();
                Close();
            };
        }

        private void btnFinishTour_Click(object sender, RoutedEventArgs e)
        {
            liveTourTrackingViewModel.FinishTour();
        }
    }
}
