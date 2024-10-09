using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookingApp.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";
        private readonly Serializer<Accommodation> _serializer;
        private List<Accommodation> _accommodations;


        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = new List<Accommodation>();
            LoadAccommodations();
        }


        public List<(string, string)> GetAllLocations()
        {
            LoadAccommodations();
            return _accommodations.Select(a => a.Location).Distinct().ToList();
        }

        public List<Accommodation> FindByType(AccommodationType type)
        {
            LoadAccommodations();
            return _accommodations.Where(a => a.Type.Equals(type)).ToList();
        }

        public List<Accommodation> FindByMaxGuests(int maxGuests)
        {
            LoadAccommodations();
            return _accommodations.Where(a => a.MaxGuests >= maxGuests).ToList();
        }

        public List<Accommodation> FindByMinReservationDays(int minReservationDays)
        {
            LoadAccommodations();
            return _accommodations.Where(a => a.MinReservationDays <= minReservationDays).ToList();
        }

        public List<Accommodation> FindByName(string name)
        {
            return _accommodations
                .Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        public List<Accommodation> FindByCity(string city)
        {
            return _accommodations
                .Where(a => a.Location.City.Contains(city, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Accommodation> FindByCountry(string country)
        {
            return _accommodations
                .Where(a => a.Location.Country.Contains(country, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void LoadAccommodations()
        {
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public List<Accommodation> GetAll()
        {
            LoadAccommodations();
            return _accommodations;
        }

        public bool CheckIfExist(string name, (string City, string Country)location, AccommodationType type)
        {
            LoadAccommodations();
            return _accommodations.Exists(a => a.Name == name && a.Location.City == location.City && a.Location.Country == location.Country && a.Type == type);
        }
        public Accommodation Save(Accommodation accommodation)
        {
            LoadAccommodations();
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            SaveAccommodations();
            return accommodation;
        }

        public Accommodation FindById(int id)
        {
            LoadAccommodations();
            return _accommodations.FirstOrDefault(a => a.Id == id);
        }

        private void SaveAccommodations()
        {
            _serializer.ToCSV(FilePath, _accommodations);
        }

        public int NextId()
        {
            LoadAccommodations();
            return _accommodations.Count < 1 ? 1 : _accommodations.Max(a => a.Id) + 1;
        }
        public List<Accommodation> GetAllByOwner(int ownerId)
        {
            LoadAccommodations();
            return _accommodations.Where(a => a.OwnerId == ownerId).ToList();
        }
    }
}
