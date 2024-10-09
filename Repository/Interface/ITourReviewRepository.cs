using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface ITourReviewRepository
    {
        List<TourReview> GetAllByScheduledTourId(int id);
        TourReview Save(TourReview tourReview);
        void Update(TourReview tourReview);
    }
}
