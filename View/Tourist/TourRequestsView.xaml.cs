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
    public partial class TourRequestsView : Window
    {
        private readonly User user;
        private readonly TourRequestsViewModel _tourRequestsViewModel;

        public TourRequestsView(User user)
        {
            InitializeComponent();
            this.user = user;
            _tourRequestsViewModel = new TourRequestsViewModel(user);
            DataContext = _tourRequestsViewModel;
        }
    }
}
