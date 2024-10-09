using System;
using System.Linq;
using BookingApp.Dto;
using BookingApp.Model;

namespace BookingApp.ViewModel.Guide
{
    public class LiveTourTrackingViewModel : ViewModelBase
    {
        public event EventHandler<TourDto> IsLiveTourTrackingFinished;

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

        private int _selectedKeyPointIndex;
        public int SelectedKeyPointIndex
        {
            get => _selectedKeyPointIndex;
            set
            {
                UpdateKeyPointMarks(value);

                _selectedKeyPointIndex = value;
                OnPropertyChanged(nameof(SelectedKeyPointIndex));
            }
        }

        private void UpdateKeyPointMarks(int value)
        {
            TourDto.Tour.KeyPoints[value].IsMarked = true;
            TourDto.Tour.KeyPoints[_selectedKeyPointIndex].IsMarked = false;
            tourService.Update(TourDto.Tour);

            if (IsTourFinished(value))
            {
                FinishTour();
            }
        }
        private bool IsTourFinished(int value) => value == TourDto.Tour.KeyPoints.Count - 1;

        private TourService tourService = new TourService();
        private ScheduledTourService scheduledTourService = new ScheduledTourService();

        public LiveTourTrackingViewModel(TourDto tourDto)
        {
            TourDto = tourDto;
        }

        public void FinishTour()
        {
            UpdateTourDto();
            tourService.Update(TourDto.Tour);
            scheduledTourService.Update(TourDto.ScheduledTour);

            IsLiveTourTrackingFinished?.Invoke(this, TourDto);
        }

        private void UpdateTourDto()
        {
            TourDto.ScheduledTour.Status = Status.Finished;
            ResetKeyPointMarks();
            FilterMarkableTourists();
        }

        private void ResetKeyPointMarks()
        {
            TourDto.Tour.KeyPoints.ForEach(kp => kp.IsMarked = false);
        }

        private void FilterMarkableTourists()
        {
            TourDto.ScheduledTour.Tourists = TourDto.ScheduledTour.Tourists.Where(t => t.IsMarkable)
                                                .ToList();
        }
    }
}
