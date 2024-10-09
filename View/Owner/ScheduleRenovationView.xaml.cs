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
    /// Interaction logic for ScheduleRenovationView.xaml
    /// </summary>
    public partial class ScheduleRenovationView : Page
    {
        private readonly ScheduleRenovationViewModel _viewModel;
        public ScheduleRenovationView(int accommodationId, NavigationService navService)
        {
            InitializeComponent();
            _viewModel = new ScheduleRenovationViewModel(accommodationId, navService);
            DataContext = _viewModel;
        }
    }
}
