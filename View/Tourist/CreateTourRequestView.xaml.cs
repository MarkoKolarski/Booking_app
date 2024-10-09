using BookingApp.Model;
using BookingApp.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.Tourist
{
    public partial class CreateTourRequestView : Window
    {
        private readonly User user;
        private readonly CreateTourRequestViewModel _createTourRequestViewModel;

        public CreateTourRequestView(User user)
        {
            InitializeComponent();
            this.user = user;
            _createTourRequestViewModel = new CreateTourRequestViewModel(user);
            DataContext = _createTourRequestViewModel;
        }

        private void CreateTourRequestButton_Click(object sender, RoutedEventArgs e)
        {
            _createTourRequestViewModel.CreateTourRequest();
            Close();
        }

        private void OpenTourRequestsPage_Click(object sender, RoutedEventArgs e)
        {
            TourRequestsView tourRequestesView = new TourRequestsView(user);
            tourRequestesView.Show();
        }

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            DateTime parsedDateTime;
            if (!DateTime.TryParse(e.Text, out parsedDateTime))
            {
                e.Handled = true;
            }
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            _createTourRequestViewModel.AddParticipant();
        }
    }

}
