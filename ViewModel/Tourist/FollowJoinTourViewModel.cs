using BookingApp.Dto;
using BookingApp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingApp.ViewModel.Tourist
{
    public class FollowJoinTourViewModel : ViewModelBase
    {
        private readonly ScheduledTourService _scheduledTourService;
        private readonly User user;
        private ObservableCollection<TourDto> _tourDtos;

        public ObservableCollection<TourDto> TourDtos
        {
            get { return _tourDtos; }
            set
            {
                if (_tourDtos != value)
                {
                    _tourDtos = value;
                    OnPropertyChanged(nameof(TourDtos));
                }
            }
        }

        public FollowJoinTourViewModel(User user)
        {
            this.user = user;
            _scheduledTourService = new ScheduledTourService();
            TourDtos = new ObservableCollection<TourDto>();
            ShowAllLiveTours();
        }

        public void ShowAllLiveTours()
        {
            List<ScheduledTour> allLiveTours = _scheduledTourService.GetAllByStatusAndTouristId(Status.Live, user.Id);
            TourService tourService = new TourService();
            foreach (ScheduledTour scheduledTour in allLiveTours)
            {
                Tour tour = tourService.GetById(scheduledTour.TourId);
                if (tour != null)
                {
                    TourDto tourDto = new TourDto(tour, scheduledTour);
                    TourDtos.Add(tourDto);
                }
            }
        }

        public void JoinTour(TourDto selectedTourDto)
        {
            if (selectedTourDto == null)
            {
                MessageBox.Show("Please select a tour before joining.");
                return;
            }

            foreach (var tourist in selectedTourDto.ScheduledTour.Tourists)
            {
                if (tourist.IsMarkable && user.Id == tourist.TouristId)
                {
                    MessageBox.Show("You have already joined this tour!");
                    return;
                }
                else if (!tourist.IsMarkable && user.Id == tourist.TouristId)
                    tourist.IsMarkable = true;
            }

            _scheduledTourService.Update(selectedTourDto.ScheduledTour);
            _scheduledTourService.NotifyJoinedTourist(selectedTourDto.ScheduledTour);

            MessageBox.Show("You have successfully joined the tour!");
        }

    }
}
