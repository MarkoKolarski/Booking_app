using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    class ShowAllAccommodationsViewModel
    {
        private readonly AccommodationService _accommodationService;
        public ObservableCollection<AccommodationDto> Accommodations { get; set; }
        public RelayCommand ShowScheduleRenovation { get; set; }
        public NavigationService NavService { get; set; }
        private int ownerId { get; set;}
        public ShowAllAccommodationsViewModel(int currentUserId, NavigationService navService)
        {
            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<AccommodationDto>();
            NavService = navService;
            ownerId = currentUserId;
            LoadAccommodations();
            ShowScheduleRenovation = new RelayCommand(ShowScheduleRenovationExecute);
        }

        private void LoadAccommodations()
        {
            var accommodations = _accommodationService.GetAllByOwner(ownerId).Select(a => new AccommodationDto
            {
                Id = a.Id,
                OwnerId = a.OwnerId,
                Name = a.Name,
                Location = a.Location,
                Type = a.Type,
                MaxGuests = a.MaxGuests,
                MinReservationDays = a.MinReservationDays,
                CancellationDays = a.CancellationDays,
                Pictures = a.Pictures
            }).ToList();
            Accommodations = new ObservableCollection<AccommodationDto>(accommodations);
        }

        public void ShowScheduleRenovationExecute(object parameter)
        {
            AccommodationDto accommodation = (AccommodationDto)parameter;
            ScheduleRenovationView scheduleRenovationView = new ScheduleRenovationView(accommodation.Id, NavService);
            NavService.Navigate(scheduleRenovationView);
        }
    }
}
