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
    /// Interaction logic for ReservationInformationView.xaml
    /// </summary>
    public partial class ReservationInformationView : Window
    {
        private readonly User user;
        private readonly ReservationInformationViewModel _viewModel;

        public ReservationInformationView(User user, Reservation selectedReservation)
        {
            InitializeComponent();
            this.user = user;
            _viewModel = new ReservationInformationViewModel(user, selectedReservation);
            DataContext = _viewModel;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }
    }
}
