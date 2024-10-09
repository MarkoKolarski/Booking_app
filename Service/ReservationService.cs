using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;

namespace BookingApp.Service
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService()
        {
            _reservationRepository = Injector.CreateInstance<IReservationRepository>();
        }

        public bool HasVisitedLocation(int userId, string location)
        {
            return _reservationRepository.GetAll()
                .Any(r => r.Guest != null &&
                          r.Accommodation != null &&
                          string.Equals($"{r.Accommodation.Location.City}, {r.Accommodation.Location.Country}", location, StringComparison.OrdinalIgnoreCase) &&
                          r.Guest.Id == userId &&
                          r.ReservedDate.Item2 < DateTime.Now);
        }

        public List<Reservation> GetReservationsByAccommodationId(int id)
        {
            return _reservationRepository.GetReservationsByAccommodationId(id);
        }

        public List<Reservation> GetByGuestId(int id)
        {
            return _reservationRepository.GetByGuestId(id);
        }


        public List<Reservation> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public Reservation GetById(int id)
        {
            return _reservationRepository.GetById(id);
        }

        public Reservation Save(Reservation reservation)
        {
            return _reservationRepository.Save(reservation);
        }


        public Reservation Update(Reservation reservation)
        {
            return _reservationRepository.Update(reservation);
        }
      

        public int NextId()
        {
            return _reservationRepository.NextId();
        }

        public List<Reservation> GetLastCheckouts(int ownerId, DateTime date)
        {
            return _reservationRepository.GetLastCheckouts(ownerId, date);
        }

        public bool CheckIfCanBeRated(Reservation reservation)
        {
            return _reservationRepository.CheckIfCanBeRated(reservation);
        }

        public bool CheckForUnratedReservation()
        {
            return _reservationRepository.CheckForUnratedReservation();
        }

        public void Delete(Reservation reservation)
        {
            _reservationRepository.Delete(reservation);
        }

        public Reservation GetReservationById(int id)
        {
            return _reservationRepository.GetReservationById(id);
        }
        
        public int CountReservationsByAccommodation(int accommodationId)
        {
            return _reservationRepository.CountReservationsByAccommodation(accommodationId);
        }
        public List<Tuple<DateTime, DateTime>> GetReservedDatesByAccommodation(int accommodationId)
        {
            return _reservationRepository.GetReservedDatesByAccommodation(accommodationId);
        }
        public List<int> GetYearsByAccommodation(int accommodationId)
        {
            return _reservationRepository.GetYearsByAccommodation(accommodationId);
        }
        public int CountReservationByYearAndAccommodation(int year, int accommodationId)
        {
            return _reservationRepository.CountReservationByYearAndAccommodation(year, accommodationId);
        }
        public List<int> GetMonthsByAccommodationAndYear(int accommodationId, int year)
        {
            return _reservationRepository.GetMonthsByAccommodationAndYear(accommodationId, year);
        }
        public int CountReservationsByMonthAndAccommodation(int month, int year, int accommodationId)
        {
            return _reservationRepository.CountReservationsByMonthAndAccommodation(month, year, accommodationId);
        }
        public int CountRejectedByYearAndAccommodation(int year, int accommodationId)
        {
            return _reservationRepository.CountRejectedByYearAndAccommodation(year, accommodationId);
        }
        public int CountRejectedByMonthAndAccommodation(int month, int year, int accommodationId)
        {
            return _reservationRepository.CountRejectedByMonthAndAccommodation(month, year, accommodationId);
        }
    }
}