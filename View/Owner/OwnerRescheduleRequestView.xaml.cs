using BookingApp.Model;
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
    public partial class OwnerRescheduleRequestView : Page
    {
        private readonly OwnerRescheduleRequestViewModel _ownerRescheduleRequestViewModel;
        public OwnerRescheduleRequestView(User user, NavigationService navService)
        {
            InitializeComponent();
            _ownerRescheduleRequestViewModel = new OwnerRescheduleRequestViewModel(user, navService);
            DataContext = _ownerRescheduleRequestViewModel;
        }
    }
}
