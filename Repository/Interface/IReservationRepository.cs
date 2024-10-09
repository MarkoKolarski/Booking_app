using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    internal interface IReservationRepository
    {
        List<Reservation> GetReservationsByAccommodationId(int id);
        List<Reservation> GetByGuestId(int id);
        List<Reservation> GetAll();
        Reservation GetById(int id);
        Reservation Save(Reservation reservation);
        Reservation Update(Reservation reservation);
        int NextId();
        List<Reservation> GetLastCheckouts(int ownerId, DateTime date);
        bool CheckIfCanBeRated(Reservation reservation);
        bool CheckForUnratedReservation();
        void Delete(Reservation reservation);
        Reservation GetReservationById(int id);
        int CountReservationsByAccommodation(int accommodationId);
        int CountNumberOfGuestByAccommodation(int accommodationId);
        List<int> GetYearsByAccommodation(int accommodationId);
        int CountReservationByYearAndAccommodation(int year, int accommodationId);
        List<Tuple<DateTime, DateTime>> GetReservedDatesByAccommodation(int accommodationId);
        List<int> GetMonthsByAccommodationAndYear(int accommodationId, int year);
        int CountReservationsByMonthAndAccommodation(int month, int year, int accommodationId);
        int CountRejectedByMonthAndAccommodation(int month, int year, int accommodationId);
        int CountRejectedByYearAndAccommodation(int year, int accommodationId);
    }
}