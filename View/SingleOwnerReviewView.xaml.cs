using BookingApp.ViewModel.Owner;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View
{
    public partial class SingleOwnerReviewView : Page
    {
        private readonly SingleOwnerReviewViewModel _viewModel;
        public SingleOwnerReviewView(int ratingId, NavigationService navService)
        {
            InitializeComponent();
            _viewModel = new SingleOwnerReviewViewModel(ratingId, navService);
            DataContext = _viewModel;
        }
    }
}
