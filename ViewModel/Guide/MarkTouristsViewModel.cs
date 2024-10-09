using System.Collections.ObjectModel;
using System.Linq;
using BookingApp.Dto;

namespace BookingApp.ViewModel.Guide
{
    public class MarkTouristsViewModel : ViewModelBase
    {
        private TourDto _tourDto;
        public TourDto TourDto
        {
            get => _tourDto;
            set
            {
                _tourDto = value;
                OnPropertyChanged(nameof(_tourDto));
            }
        }

        public ObservableCollection<string> KeyPointNames { get; set; }

        ScheduledTourService _scheduledTourService = new ScheduledTourService();

        private readonly string didNotCome = "Nije došao/la";

        public MarkTouristsViewModel(TourDto tourDto)
        {
            TourDto = tourDto;
            InitializeKeyPointNames();
        }

        private void InitializeKeyPointNames()
        {
            KeyPointNames = new ObservableCollection<string>(TourDto.Tour.KeyPoints.Select(kp => kp.Name));
            KeyPointNames.Insert(0, didNotCome);
        }

        public void FinishMarkingTourists()
        {
            FilterPresentTourists();
            _scheduledTourService.Update(TourDto.ScheduledTour);
        }

        private void FilterPresentTourists()
        {
            TourDto.ScheduledTour.Tourists = TourDto.ScheduledTour.Tourists.Where(t => t.KeyPointName != "" && t.KeyPointName != didNotCome)
                                                .ToList();
        }
    }
}
