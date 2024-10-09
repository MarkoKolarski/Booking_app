using BookingApp.Model;
using BookingApp.ViewModel.Guest;
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

namespace BookingApp.View.Guest
{
    /// <summary>
    /// Interaction logic for OwnerRatingView.xaml
    /// </summary>
    public partial class OwnerRatingsView : Window
    {
        private User user;
        //private readonly RateAccommodationViewModel viewModel;
        private readonly OwnerRatingsViewModel _viewModel;
        public OwnerRatingsView(User user)
        {
            InitializeComponent();
            this.user = user;
            _viewModel = new OwnerRatingsViewModel(user);
            DataContext = _viewModel;

        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

    }
}
