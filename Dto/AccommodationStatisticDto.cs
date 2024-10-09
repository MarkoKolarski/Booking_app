using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class AccommodationStatisticDto : ViewModelBase
    {
        private int accommodationId;
        private string accommodationName;
        private (string City, string Country) location;
        private int year;
        private int month;
        private int numberOfReservations;
        private int numberOfRejections;
        private int numberOfGuests;
        private int numberOfPostponded;
        private int numberOfSuggestions;

        public int AccommodationId
        {
            get => accommodationId;
            set
            {
                accommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
            }
        }

        public string AccommodationName
        {
            get => accommodationName;
            set
            {
                accommodationName = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }

        public int Year
        {
            get => year;
            set
            {
                year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public int Month
        {
            get => month;
            set
            {
                month = value;
                OnPropertyChanged(nameof(Month));
            }
        }

        public int NumberOfReservations
        {
            get => numberOfReservations;
            set
            {
                numberOfReservations = value;
                OnPropertyChanged(nameof(NumberOfReservations));
            }
        }

        public int NumberOfRejections
        {
            get => numberOfRejections;
            set
            {
                numberOfRejections = value;
                OnPropertyChanged(nameof(numberOfRejections));
            }
        }

        public int NumberOfGuests
        {
            get => numberOfGuests;
            set
            {
                numberOfGuests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }
        public int NumberOfPostponded
        {
            get => numberOfPostponded;
            set
            {
                numberOfPostponded = value;
                OnPropertyChanged(nameof(NumberOfPostponded));
            }
        }

        public int NumberOfSuggestions
        {
            get => numberOfSuggestions;
            set
            {
                numberOfSuggestions = value;
                OnPropertyChanged(nameof(NumberOfSuggestions));
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

   

        public AccommodationStatisticDto(int accommodationId, string accommodationName, int numberOfReservations, (string City, string Country) location)
        {
            AccommodationId = accommodationId;
            AccommodationName = accommodationName;
            NumberOfReservations = numberOfReservations;
            Location = location;
        }

        public AccommodationStatisticDto(int accommodationId, string accommodationName, int year , int numberOfReservations, int numberOfPostponded, int numberOfSugestions, int numberOfRejected)
        {
            AccommodationId = accommodationId;
            AccommodationName = accommodationName;
            NumberOfReservations = numberOfReservations;
            Year = year;
            NumberOfPostponded = numberOfPostponded;
            NumberOfSuggestions = numberOfSugestions;
            NumberOfRejections = numberOfRejected;
        }

        public AccommodationStatisticDto(int accommodationId, string accommodationName, int year,int month, int numberOfReservations, int numberOfPostponded,int numberOfSugestions, int numberOfRejections)
        {
            AccommodationId = accommodationId;
            AccommodationName = accommodationName;
            NumberOfReservations = numberOfReservations;
            Year = year;
            Month = month;
            NumberOfPostponded = numberOfPostponded;
            NumberOfSuggestions = numberOfSugestions;
            NumberOfRejections = numberOfRejections;
        }
    }
}
