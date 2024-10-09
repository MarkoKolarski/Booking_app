using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface ITourRepository
    {
        List<Tour> GetAll();
        List<Tour> GetAllByScheduledTours(List<ScheduledTour> scheduledTours);
        List<Tour> GetAllByLocation(string location);
        Tour GetById(int id);
        Tour Save(Tour tour);
        void Update(Tour tour);
        void NotifyNewTour(List<Tour> tours);
    }
}
