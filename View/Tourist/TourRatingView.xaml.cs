using BookingApp.Model;
using BookingApp.ViewModel.Tourist;
using System.Windows;

namespace BookingApp.View.Tourist
{
    public partial class TourRatingView : Window
    {

        private TourRatingViewModel tourRatingViewModel;
        private readonly User user;

        public TourRatingView(User user)
        {
            InitializeComponent();
            this.user = user;
            tourRatingViewModel = new TourRatingViewModel(user);
            DataContext = tourRatingViewModel;
            tourRatingViewModel.CloseWindow += CloseWindowHandler;
        }

        private void SubmitRatingButton_Click(object sender, RoutedEventArgs e)
        {
            tourRatingViewModel.SubmitRating();
        }

        private void BrowseImagesButton_Click(object sender, RoutedEventArgs e)
        {
            tourRatingViewModel.UploadImages();
        }

        private void CloseWindowHandler()
        {
            this.Close();
        }
    }
}
