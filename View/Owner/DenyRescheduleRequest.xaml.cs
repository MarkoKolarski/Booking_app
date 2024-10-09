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
    public partial class DenyRescheduleRequest : Page
    {
        private readonly DenyRescheduleRequestViewModel _denyRescheduleRequestViewModel;
        public DenyRescheduleRequest(int rescheduleReservationRequestId, NavigationService navService)
        {
            InitializeComponent();
            _denyRescheduleRequestViewModel = new DenyRescheduleRequestViewModel(rescheduleReservationRequestId, navService);
            DataContext = _denyRescheduleRequestViewModel;
        }
    }
}
