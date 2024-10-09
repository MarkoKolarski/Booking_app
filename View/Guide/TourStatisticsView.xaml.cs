using System.Windows;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for TourStatisticsView.xaml
    /// </summary>
    public partial class TourStatisticsView : Window
    {
        public TourStatisticsView(TourDto tourDto)
        {
            InitializeComponent();
            DataContext = new TourStatisticsViewModel(tourDto);
        }
    }
}
