using BookingApp.Model;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class AccommodationDto : ViewModelBase
    {
        private int id;
        private int ownerId;
        private string name;
        private (string City, string Country) location;
        private AccommodationType type;
        private int maxGuests;
        private int minReservationDays;
        private int cancellationDays;
        private List<string> pictures;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int OwnerId
        {
            get => ownerId;
            set
            {
                ownerId = value;
                OnPropertyChanged(nameof(OwnerId));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public (string City, string Country) Location
        {
            get => location;
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public AccommodationType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public int MaxGuests
        {
            get => maxGuests;
            set
            {
                maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }

        public int MinReservationDays
        {
            get => minReservationDays;
            set
            {
                minReservationDays = value;
                OnPropertyChanged(nameof(MinReservationDays));
            }
        }

        public int CancellationDays
        {
            get => cancellationDays;
            set
            {
                cancellationDays = value;
                OnPropertyChanged(nameof(CancellationDays));
            }
        }

        public List<string> Pictures
        {
            get => pictures;
            set
            {
                pictures = value;
                OnPropertyChanged(nameof(Pictures));
            }
        }
        public AccommodationDto()
        {
            Name = string.Empty;
            Pictures = new List<string>();
        }

        public AccommodationDto(int id, string name, int ownerId, (string City, string Country) location, AccommodationType type, int minReservationDays, List<string> pictures, int maxGuests, int cancellationDays)
        {
            Id = id;
            Name = name;
            OwnerId = ownerId;
            Location = location;
            Type = type;
            MinReservationDays = minReservationDays;
            CancellationDays = cancellationDays;
            MaxGuests = maxGuests;
            Pictures = pictures;
        }
    }
}
