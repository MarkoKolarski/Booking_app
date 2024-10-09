using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guide;

namespace BookingApp.ViewModel.Guide
{
    public class ScheduledToursViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> TourDtos { get; set; }

        private readonly int userId;

        private TourDto _selectedTourDto;

        private TourService tourService = new TourService();
        private ScheduledTourService scheduledTourService = new ScheduledTourService();
        private VoucherService voucherService = new VoucherService();

        public ScheduledToursViewModel(int userId)
        {
            this.userId = userId;
            InitializeTourDtos();
        }

        private void InitializeTourDtos()
        {
            TourDtos = new ObservableCollection<TourDto>();
            List<ScheduledTour> scheduledTours = scheduledTourService.GetAllByStatusAndGuideId(Status.Scheduled, userId);
            List<Tour> tours = tourService.GetAllByScheduledTours(scheduledTours);

            for (int i = 0; i < scheduledTours.Count && i < tours.Count; i++)
            {
                TourDtos.Add(new TourDto(tours[i], scheduledTours[i]));
            }
        }

        public void StartTour(TourDto selectedTourDto)
        {
            _selectedTourDto = selectedTourDto;
            _selectedTourDto.Tour.KeyPoints[0].IsMarked = true;
            _selectedTourDto.ScheduledTour.Status = Status.Live;

            TourDtos.Remove(_selectedTourDto);
            tourService.Update(_selectedTourDto.Tour);
            scheduledTourService.Update(_selectedTourDto.ScheduledTour);

            LiveTourTrackingView liveTourTrackingView = new LiveTourTrackingView(_selectedTourDto);
            liveTourTrackingView.ShowDialog();
        }

        public void CancelTour(TourDto selectedTourDto)
        {
            _selectedTourDto = selectedTourDto;
            SendVouchers();
            TourDtos.Remove(_selectedTourDto);
            scheduledTourService.Delete(_selectedTourDto.ScheduledTour);
        }

        private void SendVouchers()
        {
            List<int> uniqueTouristIds = FilterUniqueTouristIds();

            foreach (int touristId in uniqueTouristIds)
            {
                voucherService.Save(new Voucher(touristId, DateTime.Now.AddYears(1)));
            }
        }

        private List<int> FilterUniqueTouristIds()
        {
            return _selectedTourDto.ScheduledTour.Tourists.Select(t => t.TouristId).Distinct().ToList();
        }
    }
}
