using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    internal interface IAccommodationRatingRepository
    {
        AccommodationRating FindById(int id);

        List<AccommodationRating> GetAllByGuestId(int guestId);
        List<AccommodationRating> GetAll();
        AccommodationRating Save(AccommodationRating rating);
        int NextId();
        void Delete(AccommodationRating rating);
        public AccommodationRating Update(AccommodationRating rating);
        public List<AccommodationRating> GetAllByOwner(int ownerId);
        public int CountByOwner(int ownerId);
        public double GetAverageRatingByOwner(int ownerId);
        int CountSuggestionsByAccommodationAndYear(int year, int accommodationId);
        int CountSuggestionsByAccommodationAndMonth(int month, int year, int accommodationId);
    }
}