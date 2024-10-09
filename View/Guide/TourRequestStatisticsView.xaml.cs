using System.Windows;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatisticsView : Window
    {
        TourRequestStatisticsViewModel tourRequestStatisticsViewModel;

        public TourRequestStatisticsView(int userId)
        {
            InitializeComponent();
            tourRequestStatisticsViewModel = new TourRequestStatisticsViewModel(userId);
            DataContext = tourRequestStatisticsViewModel;
        }

        private void LocationComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            LanguageComboBox.SelectedItem = null;
        }

        private void LanguageComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            LocationComboBox.SelectedItem = null;
        }

        private void btnMostWantedLocation_Click(object sender, RoutedEventArgs e)
        {
            tourRequestStatisticsViewModel.CreateTour("location");
        }

        private void btnMostWantedLanguage_Click(object sender, RoutedEventArgs e)
        {
            tourRequestStatisticsViewModel.CreateTour("language");
        }
    }
}
