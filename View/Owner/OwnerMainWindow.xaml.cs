using BookingApp.Model;
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
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using BookingApp.Repository;
using BookingApp.Service;
using System.Windows.Navigation;
using BookingApp.ViewModel.Owner;

namespace BookingApp.View
{
    public partial class OwnerMainWindow : Window
    {
        private readonly OwnerMainWindowViewModel _ownerMainWindowViewModel;
        public OwnerMainWindow(User user)
        {
            InitializeComponent();
            _ownerMainWindowViewModel = new OwnerMainWindowViewModel(user, this.MainFrame);
            DataContext = _ownerMainWindowViewModel;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
