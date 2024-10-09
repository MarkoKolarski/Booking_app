using System.Windows;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        private readonly int userId;

        public GuideMainWindow(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void btnCreateTourView_Click(object sender, RoutedEventArgs e)
        {
            CreateTourView createTourView = new CreateTourView(null, userId);
            createTourView.ShowDialog();
        }

        private void btnScheduledToursView_Click(object sender, RoutedEventArgs e)
        {
            ScheduledToursView scheduledToursView = new ScheduledToursView(userId);
            scheduledToursView.ShowDialog();
        }

        private void btnFinishedToursView_Click(object sender, RoutedEventArgs e)
        {
            FinishedToursView finishedToursView = new FinishedToursView(userId);
            finishedToursView.ShowDialog();
        }

        private void btnTourRequestsView_Click(object sender, RoutedEventArgs e)
        {
            TourRequestsView tourRequestsView = new TourRequestsView(userId);
            tourRequestsView.ShowDialog();
        }

        private void btnTourRequestStatisticsView_Click(object sender, RoutedEventArgs e)
        {
            TourRequestStatisticsView tourRequestStatisticsView = new TourRequestStatisticsView(userId);
            tourRequestStatisticsView.ShowDialog();
        }
    }
}
