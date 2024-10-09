using System.Collections.Generic;
using BookingApp.Model;
using BookingApp.Repository.Interface;

public class TourService
{
    private readonly ITourRepository _tourRepository;

    public TourService()
    {
        _tourRepository = Injector.CreateInstance<ITourRepository>();
    }

    public List<Tour> GetAll()
    {
        return _tourRepository.GetAll();
    }

    public List<Tour> GetAllByScheduledTours(List<ScheduledTour> scheduledTours)
    {
        return _tourRepository.GetAllByScheduledTours(scheduledTours);
    }

    public List<Tour> GetAllByLocation(string location)
    {
        return _tourRepository.GetAllByLocation(location);
    }

    public Tour GetById(int id)
    {
        return _tourRepository.GetById(id);
    }

    public void NotifyNewTour(List<Tour> tours)
    {         
        _tourRepository.NotifyNewTour(tours);
    }

    public Tour Save(Tour tour)
    {
        return _tourRepository.Save(tour);
    }

    public void Update(Tour tour)
    {
        _tourRepository.Update(tour);
    }


}
