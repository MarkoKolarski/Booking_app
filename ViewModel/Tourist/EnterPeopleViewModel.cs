using BookingApp.Model;
using BookingApp.Dto;
using System.Collections.Generic;
using System.Windows;

namespace BookingApp.ViewModel.Tourist
{
    public class EnterPeopleViewModel : ViewModelBase
    {
        private ScheduledTour _scheduledTour;
        private Tour _tour;
        private List<Model.Tourist> _participants = new List<Model.Tourist>();
        public int _enteredParticipantsCount = 0;
        private readonly TourService _tourService = new TourService();
        private readonly ScheduledTourService _scheduledTourService = new ScheduledTourService();
        private readonly User user;

        public EnterPeopleViewModel(User user, ScheduledTour tour)
        {
            this.user = user;
            _scheduledTour = tour;
            _tour = _tourService.GetById(_scheduledTour.TourId);
        }

        public EnterPeopleViewModel(User user, ScheduledTour tour, List<Model.Tourist> participants)
        {
            this.user = user;
            _scheduledTour = tour;
            _tour = _tourService.GetById(_scheduledTour.TourId);
            _participants = participants;
        }

        public int TouristId { get; set; }
        public string NumberOfPeople { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public List<Model.Tourist> Participants => _participants;

        private VoucherDto _voucher;

        public VoucherDto Voucher
        {
            get { return _voucher; }
            set
            {
                _voucher = value;
                OnPropertyChanged(nameof(Voucher));
            }
        }

        public bool TourIsFullyBooked()
        {
            return _scheduledTour.FreeSpots == 0;
        }

        public int AvailableSpots()
        {
            return _scheduledTour.FreeSpots;
        }

        public void MakeReservation()
        {
            ScheduledTourService scheduledTourService = new ScheduledTourService();
            _scheduledTour.Tourists.AddRange(_participants);
            _scheduledTour.FreeSpots -= _participants.Count;
            scheduledTourService.Update(_scheduledTour);

            MessageBox.Show("Reservation submitted successfully!", "Reservation", MessageBoxButton.OK, MessageBoxImage.Information);

            _enteredParticipantsCount = 0;
            NumberOfPeople = "";
            FirstName = "";
            LastName = "";
            Age = "";

        }

        public bool ValidateInput()
        {
            string firstName = FirstName?.Trim();
            string lastName = LastName?.Trim();
            int age;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || !int.TryParse(Age, out age))
            {
                MessageBox.Show("Please, enter valid infomation about participant.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public void AddParticipant()
        {
            if (!ValidateInput())
                return;

            string firstName = FirstName.Trim();
            string lastName = LastName.Trim();
            int age = int.Parse(Age);
            TouristId = user.Id;

            _participants.Add(new Model.Tourist(TouristId, firstName, lastName, age, "", false));
            _enteredParticipantsCount++;
            MessageBox.Show("Participant added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            FirstName = "";
            LastName = "";
            Age = "";
        }

        public void CancelReservation()
        {
            _enteredParticipantsCount = 0;
            _participants.Clear();
            NumberOfPeople = "";
            FirstName = "";
            LastName = "";
            Age = "";
        }

        public List<TourDto> ShowOtherToursOnSameLocation()
        {
            List<Tour> otherTours = _tourService.GetAllByLocation(_tour.Location);
            List<ScheduledTour> otherScheduledTours = _scheduledTourService.GetOtherScheduledTours(otherTours, _scheduledTour);

            List<TourDto> otherTourDtos = new List<TourDto>();

            foreach (ScheduledTour scheduledTour in otherScheduledTours)
            {
                Tour tour = _tourService.GetById(scheduledTour.TourId);
                if (tour != null && scheduledTour.Status == Status.Scheduled)
                {
                    TourDto tourDto = new TourDto(tour, scheduledTour);
                    otherTourDtos.Add(tourDto);
                }
            }

            return otherTourDtos;
        }
    }
}
