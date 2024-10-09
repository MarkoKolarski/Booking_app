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
    /// <summary>
    /// Interaction logic for ShowAllRenovationView.xaml
    /// </summary>
    public partial class ShowAllRenovationView : Page
    {
        private readonly ShowAllRenovationsViewModel _showAllRenovationViewModel;
        public ShowAllRenovationView()
        {
            InitializeComponent();
            _showAllRenovationViewModel = new ShowAllRenovationsViewModel();
            DataContext = _showAllRenovationViewModel;
        }
    }
}
