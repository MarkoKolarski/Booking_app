using BookingApp.Repository;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.Service
{
    public class AccommodationRatingService
    {
        private readonly IAccommodationRatingRepository _accommodationRatingRepository;

        public AccommodationRatingService()
        {
            _accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
        }


        public List<AccommodationRating> GetAllByGuestId(int guestId)
        {
            return _accommodationRatingRepository.GetAllByGuestId(guestId);
        }



        public List<AccommodationRating> GetAllRatings()
        {
            return _accommodationRatingRepository.GetAll();
        }

        public AccommodationRating SaveRating(AccommodationRating rating)
        {
            return _accommodationRatingRepository.Save(rating);
        }

        public int GetNextId()
        {
            return _accommodationRatingRepository.NextId();
        }

        public void DeleteRating(AccommodationRating rating)
        {
            _accommodationRatingRepository.Delete(rating);
        }

        public AccommodationRating UpdateRating(AccommodationRating rating)
        {
            return _accommodationRatingRepository.Update(rating);
        }
        public List<AccommodationRating> GetAllByOwnerId(int ownerId)
        {
            return _accommodationRatingRepository.GetAllByOwner(ownerId);
        }
        public int CountByOwner(int ownerId)
        {
            return _accommodationRatingRepository.CountByOwner(ownerId);
        }
        public double GetAverageRatingByOwner(int ownerId)
        {
            return _accommodationRatingRepository.GetAverageRatingByOwner(ownerId) / 2;
        }
        
        public AccommodationRating GetById(int id)
        {
            return _accommodationRatingRepository.FindById(id);
        }
        public int CountSuggestionsByAccommodationAndYear(int year, int accommodationId)
        {
            return _accommodationRatingRepository.CountSuggestionsByAccommodationAndYear(year, accommodationId);
        }
        public int CountSuggestionsByAccommodationAndMonth(int month, int year, int accommodationId)
        {
            return _accommodationRatingRepository.CountSuggestionsByAccommodationAndMonth(month, year, accommodationId);
        }
    }

}