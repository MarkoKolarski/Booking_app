using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.ViewModel;

namespace BookingApp.View
{
    public partial class GuestRatingView : Page
    {
        private readonly GuestRatingViewModel _viewModel;

        public GuestRatingView(int reservationId, int ownerId)
        {
            InitializeComponent();
            _viewModel = new GuestRatingViewModel(reservationId, ownerId);
            DataContext = _viewModel;
        }

        private void SubmitRating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.SubmitRating(sender);
                MessageBox.Show("Rating submitted successfully!");
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
