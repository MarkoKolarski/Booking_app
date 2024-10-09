using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Tourist
{
    public class CreateTourRequestViewModel : ViewModelBase
    {
        private readonly User user;
        private TourRequest _tourRequest;
        private readonly TourRequestService _tourRequestService = new TourRequestService();
        private int _participantsCount;

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(_location));
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(_description));
            }
        }
        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged(nameof(_language));
            }
        }
        private int _touristNumber;
        public int TouristNumber 
        { 
            get => _touristNumber;
            set
            {
                _touristNumber = value;
                OnPropertyChanged(nameof(_touristNumber));
            }
        }

        private List<Model.Tourist> _participants;
        public List<Model.Tourist> Participants 
        { 
            get => _participants;
            set
            {
                _participants = value;
                OnPropertyChanged(nameof(_participants));
            }
        }

        private string _participantName;
        public string ParticipantName
        {
            get => _participantName;
            set
            {
                _participantName = value;
                OnPropertyChanged(nameof(ParticipantName));
            }
        }

        private string _participantSurname;
        public string ParticipantSurname
        {
            get => _participantSurname;
            set
            {
                _participantSurname = value;
                OnPropertyChanged(nameof(ParticipantSurname));
            }
        }

        private int _participantAge;
        public int ParticipantAge
        {
            get => _participantAge;
            set
            {
                _participantAge = value;
                OnPropertyChanged(nameof(ParticipantAge));
            }
        }

        public DateTime Now { get; } = DateTime.Now;

        private string _start;
        public string Start
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged(nameof(_start));
            }
        }
        private string _end;
        public string End
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged(nameof(_end));
            }
        }

        private LanguageService languageService = new LanguageService();
        private LocationService locationService = new LocationService();

        public List<string> Languages { get; }
        public List<string> Locations { get; }

        public CreateTourRequestViewModel(User user)
        {
            this.user = user;
            Locations = locationService.GetAll();
            Languages = languageService.GetAll();
            Participants = new List<Model.Tourist>();
            _participantsCount = 0;
        }

        public void CreateTourRequest()
        {
            if (!string.IsNullOrWhiteSpace(Location) && !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Language) && TouristNumber > 0 && Participants.Count == TouristNumber && !string.IsNullOrWhiteSpace(Start) && !string.IsNullOrWhiteSpace(End))
            {
                TourRequest newTourRequest = new TourRequest(Location, Description, Language, TouristNumber, Participants, Convert.ToDateTime(Start), Convert.ToDateTime(End), TourRequestStatus.Waiting);
                _tourRequestService.Save(newTourRequest);

                ClearRequestFields();

                MessageBox.Show("Tour request created successfully!");
            }
            else
            {
                MessageBox.Show("Please fill all fields correctly and ensure that the number of participants matches the specified tourist number.");
            }
        }
        public void AddParticipant()
        {
  
            if (_participantsCount < TouristNumber && !string.IsNullOrWhiteSpace(ParticipantName) && !string.IsNullOrWhiteSpace(ParticipantSurname) && ParticipantAge > 0)
            {
                Participants.Add(new Model.Tourist(user.Id, ParticipantName, ParticipantSurname, ParticipantAge, "", false));
                ClearParticipantFields();
                OnPropertyChanged(nameof(Participants));
                _participantsCount++;
                MessageBox.Show("Participant added successfully!");
            }
            else if(_participantsCount >= TouristNumber)
            {
                MessageBox.Show("You have reached the maximum number of participants.");
            }
            else if (string.IsNullOrWhiteSpace(ParticipantName) || string.IsNullOrWhiteSpace(ParticipantSurname) || ParticipantAge <= 0)
            {
                MessageBox.Show("Please fill all fields correctly.");
            }
     
        }

        private void ClearRequestFields()
        {
            Location = "";
            Description = "";
            Language = "";
            TouristNumber = 0;
            Participants.Clear();
            Start = "";
            End = "";
        }

        private void ClearParticipantFields()
        {
            ParticipantName = "";
            ParticipantSurname = "";
            ParticipantAge = 0;
        }

    }
}
