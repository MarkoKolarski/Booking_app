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
using System.Windows.Shapes;

namespace BookingApp.View
{
    public partial class RatingsOverview : Page
    {
        public readonly RatingsOverViewModel _ratingsOverViewModel;
         public RatingsOverview(User user)
         {
            InitializeComponent();
            _ratingsOverViewModel = new RatingsOverViewModel(user);
            DataContext = _ratingsOverViewModel;
         }
    }
}
