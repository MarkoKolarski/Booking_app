using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.ViewModel.Tourist;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.Tourist
{
    public partial class FollowJoinTourView : Window
    {
        private FollowJoinTourViewModel followJoinTourViewModel;
        private readonly User user;

        public FollowJoinTourView(User user)
        {
            InitializeComponent();
            this.user = user;
            followJoinTourViewModel = new FollowJoinTourViewModel(user);
            DataContext = followJoinTourViewModel;
        }

        private void JoinTour_Click(object sender, RoutedEventArgs e)
        {
            var selectedTourDto = (sender as Button)?.DataContext as TourDto;
            followJoinTourViewModel.JoinTour(selectedTourDto);
            Close();
        }
    }
}
