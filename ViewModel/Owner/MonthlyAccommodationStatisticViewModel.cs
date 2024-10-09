using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    internal class MonthlyAccommodationStatisticViewModel
    {
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        private readonly AccommodationRatingService _accommodationRatingService;
        public AccommodationStatisticDto YearStatistic { get; set; }
        public ObservableCollection<AccommodationStatisticDto> AccommodationMonthlyStatistics { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand GetBack { get; set; }
        public MonthlyAccommodationStatisticViewModel(AccommodationStatisticDto accommodationStatisticDto, NavigationService navService)
        {
            _accommodationService = new AccommodationService();
            _reservationService = new ReservationService();
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            _accommodationRatingService = new AccommodationRatingService();
            YearStatistic = accommodationStatisticDto;
            AccommodationMonthlyStatistics = new ObservableCollection<AccommodationStatisticDto>();
            NavService = navService;
            LoadMonthlyStatistic();
            GetBack = new RelayCommand(GetBackExecute);
        }
        private void LoadMonthlyStatistic()
        {
            LoadStatisticGroupedByMonth();
        }

        private void LoadStatisticGroupedByMonth()
        {
            var months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            foreach (var month in months)
            {
                AccommodationMonthlyStatistics.Add(new AccommodationStatisticDto(
                                       YearStatistic.AccommodationId,
                                       YearStatistic.AccommodationName,
                                       YearStatistic.Year,
                                       month,
                                       _reservationService.CountReservationsByMonthAndAccommodation(month, YearStatistic.Year, YearStatistic.AccommodationId),
                                       _rescheduleReservationRequestService.CountPostpondedByMonthAndAccommodation(month, YearStatistic.Year, YearStatistic.AccommodationId),
                                       _accommodationRatingService.CountSuggestionsByAccommodationAndMonth(month, YearStatistic.Year, YearStatistic.AccommodationId),
                                       _reservationService.CountRejectedByMonthAndAccommodation(month, YearStatistic.Year, YearStatistic.AccommodationId)
                                       ));
            }
        }

        public void GetBackExecute(object parameter)
        {
            NavService.GoBack();
        }
    }
}
