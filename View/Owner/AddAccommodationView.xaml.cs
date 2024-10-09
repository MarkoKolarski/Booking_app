using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.IO;
using System.Collections.ObjectModel;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class AddAccommodationView : Page
    {
        public readonly AddAccommodationViewModel _viewModel;

        public AddAccommodationView(User user)
        {
            InitializeComponent();
            _viewModel = new AddAccommodationViewModel(user);
            DataContext = _viewModel;
            
        }

        private void AddAccommodation(object sender, RoutedEventArgs e)
        {
            _viewModel.AddAccommodation();
        }

        private void AddImages(object sender, RoutedEventArgs e)
        {
            _viewModel.AddImages();
        }

    }
}
