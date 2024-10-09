using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    internal class ScheduleRenovationViewModel : ViewModelBase
    {
        public ObservableCollection<Tuple<DateTime, DateTime>> AvailableDates { get; set; }
        public RenovationDto Renovation { get; set; } = new RenovationDto();

        private readonly RenovationService _renovationService;
        private readonly ReservationService _reservationService;
        private readonly AccommodationService _accommodationService;

        public NavigationService NavService { get; set; }
        public RelayCommand ScheduleRenovation { get; set; }
        public RelayCommand FindAvailableDates { get; set; }
        public RelayCommand SetRenovationDates { get; set; }
        public ScheduleRenovationViewModel(int accommodationId, NavigationService navService)
        {
            _renovationService = new RenovationService();
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
            AvailableDates = new ObservableCollection<Tuple<DateTime, DateTime>>();
            LoadAccommodationDetails(accommodationId);
            NavService = navService;
            SetRenovationDates = new RelayCommand(SetRenovationDatesExecute);
            FindAvailableDates = new RelayCommand(FindAvailableDatesExecute);
            ScheduleRenovation = new RelayCommand(ScheduleRenovationExecute);
        }

        private void LoadAccommodationDetails(int accommodationId)
        {
            var accommodation = _accommodationService.FindById(accommodationId);
            Renovation.AccommodationId = accommodation.Id;
            Renovation.AccommodationName = accommodation.Name;
            Renovation.Location = accommodation.Location;
        }

        public void FindAvailableDatesExecute(object parameter)
        {
            if (Renovation.StartDate == null || Renovation.EndDate == DateTime.Today || Renovation.Lasting == 0)
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }
            GetAvailableDateRanges(Renovation.StartDate, Renovation.EndDate, Renovation.Lasting);
            ValidateAvailableDates();  
        }
        public void ValidateAvailableDates()
        {
            if (AvailableDates.Count == 0)
            {
                MessageBox.Show("No available dates found!");
            }
        }
        public void SetRenovationDatesExecute(object parameter)
        {
            var dates = (Tuple<DateTime, DateTime>)parameter;
            Renovation.StartDate = dates.Item1;
            Renovation.EndDate = dates.Item2;
            AvailableDates.Clear();
            MessageBox.Show("Dates set successfully!");
        }
        public void ScheduleRenovationExecute(object parameter)
        {
            if(Renovation.Description == string.Empty)
            {
                MessageBox.Show("Please add a description!");
                return;
            }
            Renovation renovation = _renovationService.SaveRenovation(DtoToRenovation(Renovation));
            if (renovation == null)
            {
                MessageBox.Show("Renovation could not be scheduled!");
                return;
            }
            MessageBox.Show("Renovation scheduled successfully!");
        }
        public Renovation DtoToRenovation(RenovationDto renovationDto)
        {
            return new Renovation
            {
                Id = renovationDto.Id,
                StartDate = renovationDto.StartDate,
                EndDate = renovationDto.EndDate,
                Accommodation = _accommodationService.FindById(renovationDto.AccommodationId),
                Description = renovationDto.Description,
                Lasting = renovationDto.Lasting,
                Status = RenovationStatus.Active,
                OwnerId = _accommodationService.FindById(renovationDto.AccommodationId).OwnerId
            };
        }
        public void GetAvailableDateRanges(DateTime startDate, DateTime endDate, int range)
        {
            List<Tuple<DateTime, DateTime>> reservedDates = _reservationService.GetReservedDatesByAccommodation(Renovation.AccommodationId);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsRangeAvailable(date, reservedDates, range))
                {
                    this.AvailableDates.Add(new Tuple<DateTime, DateTime>(date, date.AddDays(range - 1)));
                }
            }
        }
        private bool IsRangeAvailable(DateTime date, List<Tuple<DateTime, DateTime>> reservedDates, int range)
        {
            for (int i = 0; i < range; i++)
            {
                if(date.AddDays(range-1) > Renovation.EndDate)
                {
                    return false;
                }
                if (reservedDates.Exists(r => r.Item1 <= date.AddDays(i) && r.Item2 >= date.AddDays(i)))
                {
                    return false;
                }
            }
            return true;
        }
     }
}
