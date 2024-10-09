using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel.Owner;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for LastCheckoutsView.xaml
    /// </summary>
    public partial class LastCheckoutsView : Page
    {
        public readonly LastCheckoutsViewModel _lastCheckoutsViewModel;
        public LastCheckoutsView(User user)
        {
            InitializeComponent();
            _lastCheckoutsViewModel = new LastCheckoutsViewModel(user);
            DataContext = _lastCheckoutsViewModel;
            checkout.MouseDoubleClick += ShowGuestRatingWindow;
        }

        private void ShowGuestRatingWindow(object sender, MouseButtonEventArgs e)
        {
            _lastCheckoutsViewModel.ShowGuestRatingWindow(sender, e, checkout);
        }
    }
}
