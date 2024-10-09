using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    internal class RenovationDto : ViewModelBase
    {
        private int _id;
        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today;
        private int _accommodationId;
        private string _accommodationName;
        private string _description = string.Empty;
        private int _lasting;
        private (string City, string Country) location;
        private string _status;
        private string _buttonContent = "Collapsed";

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public int AccommodationId
        {
            get { return _accommodationId; }
            set
            {
                _accommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int Lasting
        {
            get { return _lasting; }
            set
            {
                _lasting = value;
                OnPropertyChanged(nameof(Lasting));
            }
        }

        public string AccommodationName
        {
            get { return _accommodationName; }
            set
            {
                _accommodationName = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }
        public (string City, string Country) Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }
    }
}
