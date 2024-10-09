using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface IScheduledTourRepository
    {
        List<ScheduledTour> GetAll();
        List<ScheduledTour> GetAllByStatusAndGuideId(Status status, int guideId);
        List<ScheduledTour> GetAllByStatusAndTouristId(Status status, int touristId);
        List<ScheduledTour> GetOtherScheduledTours(List<Tour> otherTours, ScheduledTour scheduledTour);
        ScheduledTour GetMostVisitedByYear(int guideId, string year);
        Tourist GetTouristById(int touristId);
        void NotifyJoinedTourist(ScheduledTour scheduledTour);
        ScheduledTour Save(ScheduledTour scheduledTour);
        void Update(ScheduledTour updatedScheduledTour);
        void Delete(ScheduledTour scheduledTour);
    }
}
