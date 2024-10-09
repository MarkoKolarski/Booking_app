using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class TourReviewRepository : ITourReviewRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReviews.csv";

        private readonly Serializer<TourReview> _serializer;

        private List<TourReview> _tourReviews;

        public TourReviewRepository()
        {
            _serializer = new Serializer<TourReview>();
            _tourReviews = _serializer.FromCSV(FilePath);
        }

        public List<TourReview> GetAllByScheduledTourId(int id)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            return _tourReviews.FindAll(tr => tr.FinishedTourId == id);
        }

        public TourReview Save(TourReview tourReview)
        {
            tourReview.Id = NextId();
            _tourReviews = _serializer.FromCSV(FilePath);
            _tourReviews.Add(tourReview);
            _serializer.ToCSV(FilePath, _tourReviews);
            return tourReview;
        }

        public int NextId()
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            return _tourReviews.Any() ? _tourReviews.Max(c => c.Id) + 1 : 1;
        }

        public void Update(TourReview tourReview)
        {
            _tourReviews = _serializer.FromCSV(FilePath);
            int index = _tourReviews.FindIndex(tr => tr.Id == tourReview.Id);
            if (index != -1)
            {
                _tourReviews[index] = tourReview;
                _serializer.ToCSV(FilePath, _tourReviews);
            }
        }
    }
}
