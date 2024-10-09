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

    public partial class SuperGuestDescriptionView : Window
    {
        private readonly SuperGuestDescriptionViewModel _superGuestDescriptionViewModel;
        private readonly User user;
        public SuperGuestDescriptionView(User user)
        {

            InitializeComponent();
            _superGuestDescriptionViewModel = new SuperGuestDescriptionViewModel(user);
            this.DataContext = _superGuestDescriptionViewModel;
        }

    }
}
