using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Service
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public bool OwnsAccommodationAtLocation(int userId, string location)
        {
            return _accommodationRepository.GetAll()
                .Any(a => a.OwnerId == userId &&
                          string.Equals($"{a.Location.City}, {a.Location.Country}", location, StringComparison.OrdinalIgnoreCase));
        }

        public List<(string, string)> GetAllLocations()
        {
            return _accommodationRepository.GetAllLocations();
        }


        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }

        public List<Accommodation> FindByName(string name)
        {
            return _accommodationRepository.FindByName(name);
        }

        public List<Accommodation> FindByCity(string city)
        {
            return _accommodationRepository.FindByCity(city);
        }

        public List<Accommodation> FindByCountry(string country)
        {
            return _accommodationRepository.FindByCountry(country);
        }

        public List<Accommodation> FindByType(AccommodationType type)
        {
            return _accommodationRepository.FindByType(type);
        }

        public List<Accommodation> FindByMaxGuests(int maxGuests)
        {
            return _accommodationRepository.FindByMaxGuests(maxGuests);
        }

        public List<Accommodation> FindByMinReservationDays(int minReservationDays)
        {
            return _accommodationRepository.FindByMinReservationDays(minReservationDays);
        }

        public bool CheckIfExist(string name, (string City, string Country) location, AccommodationType type)
        {
            return _accommodationRepository.CheckIfExist(name, location, type);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            return _accommodationRepository.Save(accommodation);
        }

        public Accommodation FindById(int id)
        {
            return _accommodationRepository.FindById(id);
        }
        
        public List<Accommodation> GetAllByOwner(int ownerId)
        {
            return _accommodationRepository.GetAllByOwner(ownerId);
        }

    }
}