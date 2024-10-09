using BookingApp.Model;
using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookingApp.ViewModel.Tourist
{
    public class TourViewModel : ViewModelBase
    {
        private readonly ScheduledTourService _scheduledTourService;

        private readonly User user;

        private ObservableCollection<TourDto> _tourDtos;
        private string _location;
        private string _language;
        private int _duration;
        private int _groupSize;

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

        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                    Search();
                }
            }
        }

        public string Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                    Search();
                }
            }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                    Search();
                }
            }
        }

        public int GroupSize
        {
            get { return _groupSize; }
            set
            {
                if (_groupSize != value)
                {
                    _groupSize = value;
                    OnPropertyChanged(nameof(GroupSize));
                    Search();
                }
            }
        }

        public TourViewModel(User user)
        {
            this.user = user;
            _scheduledTourService = new ScheduledTourService();
            TourDtos = new ObservableCollection<TourDto>();
            ShowAllTours();
        }

        private void ShowAllTours()
        {
            List<ScheduledTour> allScheduledTours = _scheduledTourService.GetAll();
            TourService tourService = new TourService();
            foreach (ScheduledTour scheduledTour in allScheduledTours)
            {
                Tour tour = tourService.GetById(scheduledTour.TourId);
                if (tour != null && scheduledTour.Status == Status.Scheduled)
                {
                    TourDto tourDto = new TourDto(tour, scheduledTour);
                    TourDtos.Add(tourDto);
                }
            }
        }

        public void Search()
        {
            TourDtos.Clear();

            List<ScheduledTour> allScheduledTours = _scheduledTourService.GetAll();
            TourService tourService = new TourService();

            foreach (ScheduledTour scheduledTour in allScheduledTours)
            {
                Tour tour = tourService.GetById(scheduledTour.TourId);

                if (IsTourMatch(tour, scheduledTour))
                {
                    TourDto tourDto = new TourDto(tour, scheduledTour);
                    TourDtos.Add(tourDto);
                }
            }
        }

        private bool IsTourMatch(Tour tour, ScheduledTour scheduledTour)
        {
            if (tour == null || scheduledTour.Status != Status.Scheduled)
                return false;

            return (string.IsNullOrEmpty(Location) || tour.Location.ToLower().Contains(Location.ToLower()))
                && (string.IsNullOrEmpty(Language) || tour.Language.ToLower().Contains(Language.ToLower()))
                && (Duration == 0 || Duration == tour.Duration)
                && (GroupSize == 0 || GroupSize == tour.MaxTourists);
        }

    }
}
