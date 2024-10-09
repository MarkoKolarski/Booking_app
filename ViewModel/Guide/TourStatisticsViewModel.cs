using System.Collections.ObjectModel;
using BookingApp.Dto;

namespace BookingApp.ViewModel.Guide
{
    public class TourStatisticsViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> _tourDto { get; set; }

        public int Kids { get; set; }
        public int Adults { get; set; }
        public int Seniors { get; set; }

        public TourStatisticsViewModel(TourDto tourDto)
        {
            _tourDto = new ObservableCollection<TourDto> { tourDto };
            (Kids, Adults, Seniors) = tourDto.ScheduledTour.CalculateTouristStatistics();
        }
    }
}
