using System.Windows;
using BookingApp.Model;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTourView.xaml
    /// </summary>
    public partial class CreateTourView : Window
    {
        private CreateTourViewModel createTourViewModel;

        public CreateTourView(TourRequest tourRequest, int userId)
        {
            InitializeComponent();
            createTourViewModel = new CreateTourViewModel(tourRequest, userId);
            DataContext = createTourViewModel;
        }

        private void btnCreateTour_Click(object sender, RoutedEventArgs e)
        {
            if (createTourViewModel.CreateTour())
            {
                Close();
            }
        }

        private void btnUploadImages_Click(object sender, RoutedEventArgs e)
        {
            createTourViewModel.UploadImages();
        }
    }
}
