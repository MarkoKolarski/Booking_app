using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    internal class AccommodationRatingRepository : IAccommodationRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationRatings.csv";

        private readonly Serializer<AccommodationRating> _serializer;

        private List<AccommodationRating> _ratings;

        public AccommodationRatingRepository()
        {
            _serializer = new Serializer<AccommodationRating>();
            _ratings = _serializer.FromCSV(FilePath);
        }


        public List<AccommodationRating> GetAllByGuestId(int guestId)
        {
            return _ratings.Where(r => r.Guest.Id == guestId).ToList();
        }

        public AccommodationRating FindById(int id)
        {
            return _ratings.FirstOrDefault(r => r.Id == id);
        }


        public List<AccommodationRating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRating Save(AccommodationRating rating)
        {
            rating.Id = NextId();
            _ratings = _serializer.FromCSV(FilePath);
            _ratings.Add(rating);
            _serializer.ToCSV(FilePath, _ratings);
            return rating;
        }

        public int NextId()
        {
            _ratings = _serializer.FromCSV(FilePath);
            if (_ratings.Count < 1)
            {
                return 1;
            }
            return _ratings.Max(r => r.Id) + 1;
        }

        public void Delete(AccommodationRating rating)
        {
            _ratings = _serializer.FromCSV(FilePath);
            AccommodationRating founded = _ratings.Find(r => r.Id == rating.Id);
            _ratings.Remove(founded);
            _serializer.ToCSV(FilePath, _ratings);
        }

        public AccommodationRating Update(AccommodationRating rating)
        {
            _ratings = _serializer.FromCSV(FilePath);
            AccommodationRating current = _ratings.Find(r => r.Id == rating.Id);
            int index = _ratings.IndexOf(current);
            _ratings.Remove(current);
            _ratings.Insert(index, rating);
            _serializer.ToCSV(FilePath, _ratings);
            return rating;
        }
        public List<AccommodationRating> GetAllByOwner(int ownerId)
        {
            return _ratings.Where(r => r.Accommodation.OwnerId == ownerId).ToList();
        }

        public int CountByOwner(int ownerId)
        {
            return _ratings.Count(r => r.Accommodation.OwnerId == ownerId);
        }
        public double GetAverageRatingByOwner(int ownerId)
        {
            return _ratings.Where(r => r.Accommodation.OwnerId == ownerId).Average(r => r.OwnerCorrectness) +
                   _ratings.Where(r => r.Accommodation.OwnerId == ownerId).Average(r => r.Cleanliness);
        }
        public int CountSuggestionsByAccommodationAndYear(int year, int accommodationId)
        {
            return _ratings.Count(r => r.Accommodation.Id == accommodationId && r.SuggestionDate.Year == year && r.RenovationLevel != RenovationLevel.None);
        }
        public int CountSuggestionsByAccommodationAndMonth(int month, int year, int accommodationId)
        {
            return _ratings.Count(r => r.Accommodation.Id == accommodationId && r.SuggestionDate.Month == month && r.SuggestionDate.Year == year && r.RenovationLevel != RenovationLevel.None);
        }
    }
}