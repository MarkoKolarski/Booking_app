using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BookingApp.View;

namespace BookingApp.ViewModel.Owner
{
    class AccommodationStatisticViewModel
    {
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        public ObservableCollection<AccommodationStatisticDto> AccommodationsStatistics { get; set; }
        public RelayCommand GetBack { get; set; }
        public RelayCommand YearlyStatistic { get; set; }
        public NavigationService NavService { get; set; }
        public AccommodationStatisticViewModel(NavigationService navService)
        {
            _accommodationService = new AccommodationService();
            _reservationService = new ReservationService();
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            NavService = navService;
            LoadAccommodationsStatistic();
            YearlyStatistic = new RelayCommand(ShowYearlyStatisticExecute);
        }

        private void LoadAccommodationsStatistic()
        {
            var accommodations = _accommodationService.GetAll().Select(a => new AccommodationStatisticDto(
                a.Id,
                a.Name,
                _reservationService.CountReservationsByAccommodation(a.Id),
                a.Location
            )).ToList();
            AccommodationsStatistics = new ObservableCollection<AccommodationStatisticDto>(accommodations);
        }

        public void ShowYearlyStatisticExecute(object parameter)
        {
            AccommodationStatisticDto accommodationStatisticDto = (AccommodationStatisticDto)parameter;
            AccommodationYearlyStatisticView accommodationYearlyStatisticView = new AccommodationYearlyStatisticView(accommodationStatisticDto, NavService);
            NavService.Navigate(accommodationYearlyStatisticView);
        }
    }
}
