using BookingApp.Dto;
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
using BookingApp.ViewModel.Owner;


namespace BookingApp.View
{
    public partial class AccommodationYearlyStatisticView : Page
    {
        private readonly AccommodationYearlyStatisticViewModel _accommodationYearlyStatisticViewModel;
        public AccommodationYearlyStatisticView(AccommodationStatisticDto accommodationStatisticDto, NavigationService navService)
        {
            InitializeComponent();
            _accommodationYearlyStatisticViewModel = new AccommodationYearlyStatisticViewModel(accommodationStatisticDto, navService);
            DataContext = _accommodationYearlyStatisticViewModel;
        }
    }
}
