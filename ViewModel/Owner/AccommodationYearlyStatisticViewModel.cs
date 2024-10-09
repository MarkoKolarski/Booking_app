using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Service;
using BookingApp.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    internal class AccommodationYearlyStatisticViewModel
    {
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        private readonly AccommodationRatingService _accommodationRatingService;
        public AccommodationStatisticDto AccommodationStatisticDto { get; set; }
        public ObservableCollection<AccommodationStatisticDto> AccommodationYearlyStatistics { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand ShowMonthlyStatistic { get; set; }
        public AccommodationYearlyStatisticViewModel(AccommodationStatisticDto accommodationStatisticDto, NavigationService navService)
        {
            _accommodationService = new AccommodationService();
            _reservationService = new ReservationService();
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            _accommodationRatingService = new AccommodationRatingService();
            AccommodationStatisticDto = accommodationStatisticDto;
            AccommodationYearlyStatistics = new ObservableCollection<AccommodationStatisticDto>();
            NavService = navService;
            LoadYearlyStatistic();
            ShowMonthlyStatistic = new RelayCommand(ShowMonthlyStatisticExecute);
        }
        private void LoadYearlyStatistic()
        {
            LoadStatisticGroupedByYear();
        }

        private void LoadStatisticGroupedByYear()
        {
            var years = _reservationService.GetYearsByAccommodation(AccommodationStatisticDto.AccommodationId);
            foreach (var year in years)
            {
                AccommodationYearlyStatistics.Add(new AccommodationStatisticDto(
                                       AccommodationStatisticDto.AccommodationId,
                                       AccommodationStatisticDto.AccommodationName,
                                       year,
                                       _reservationService.CountReservationByYearAndAccommodation(year, AccommodationStatisticDto.AccommodationId),
                                       _rescheduleReservationRequestService.CountAllByYearAndAccommodation(year, AccommodationStatisticDto.AccommodationId),
                                       _accommodationRatingService.CountSuggestionsByAccommodationAndYear(year, AccommodationStatisticDto.AccommodationId),
                                       _reservationService.CountRejectedByYearAndAccommodation(year, AccommodationStatisticDto.AccommodationId)
                                       ));
            }
        }
        public void ShowMonthlyStatisticExecute(object parameter)
        {
            AccommodationStatisticDto accommodationStatisticDto = (AccommodationStatisticDto)parameter;
            MonthlyAccommodationStatisticView monthlyStatistic = new MonthlyAccommodationStatisticView(accommodationStatisticDto, NavService);
            NavService.Navigate(monthlyStatistic);
        }
    }
}
