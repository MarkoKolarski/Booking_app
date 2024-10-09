using BookingApp.Model;
using BookingApp.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.Tourist
{
    public partial class RequestStatisticsView : Window
    {

        private readonly User user;
        private readonly RequestStatisticsViewModel _requestStatisticsViewModel;

        public RequestStatisticsView(User user)
        {
            InitializeComponent();
            this.user = user;
            _requestStatisticsViewModel = new RequestStatisticsViewModel(user);
            DataContext = _requestStatisticsViewModel;
            _requestStatisticsViewModel.GetStatistics();
        }

        private void ShowStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(YearTextBox.Text, out int year))
            {
                _requestStatisticsViewModel.Year = year;
                _requestStatisticsViewModel.GetStatistics();
            }
            else
            {
                MessageBox.Show("Please enter a valid year.");
            }
        }

        private void ShowOverallStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            _requestStatisticsViewModel.Year = 0;
            _requestStatisticsViewModel.GetStatistics();
        }

    }

}
