using BookingApp.Model;
using BookingApp.Repository.Interface;
using System.Collections.Generic;

namespace BookingApp.Service
{
    public class TourReviewService
    {
        private readonly ITourReviewRepository _tourReviewRepository;

        public TourReviewService()
        {
            _tourReviewRepository = Injector.CreateInstance<ITourReviewRepository>();
        }

        public List<TourReview> GetAllByScheduledTourId(int id)
        {
            return _tourReviewRepository.GetAllByScheduledTourId(id);
        }

        public TourReview Save(TourReview tourReview)
        {
            return _tourReviewRepository.Save(tourReview);
        }

        public void Update(TourReview tourReview)
        {
            _tourReviewRepository.Update(tourReview);
        }
    }
}
