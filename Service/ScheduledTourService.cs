using System.Collections.Generic;
using BookingApp.Model;
using BookingApp.Repository.Interface;

public class ScheduledTourService
{
    private readonly IScheduledTourRepository _scheduledTourRepository;

    public ScheduledTourService()
    {
        _scheduledTourRepository = Injector.CreateInstance<IScheduledTourRepository>();
    }

    public List<ScheduledTour> GetAll()
    {
        return _scheduledTourRepository.GetAll();
    }

    public List<ScheduledTour> GetAllByStatusAndGuideId(Status status, int guideId)
    {
        return _scheduledTourRepository.GetAllByStatusAndGuideId(status, guideId);
    }

    public List<ScheduledTour> GetAllByStatusAndTouristId(Status status, int touristId)
    {
        return _scheduledTourRepository.GetAllByStatusAndTouristId(status, touristId);
    }

    public List<ScheduledTour> GetOtherScheduledTours(List<Tour> otherTours, ScheduledTour scheduledTour)
    {
        return _scheduledTourRepository.GetOtherScheduledTours(otherTours, scheduledTour);
    }

    public ScheduledTour GetMostVisitedByYear(int guideId, string year)
    {
        return _scheduledTourRepository.GetMostVisitedByYear(guideId, year);
    }

    public Tourist GetTouristById(int touristId)
    {
        return _scheduledTourRepository.GetTouristById(touristId);
    }

    public void NotifyJoinedTourist(ScheduledTour scheduledTour)
    {
        _scheduledTourRepository.NotifyJoinedTourist(scheduledTour);
    }

    public ScheduledTour Save(ScheduledTour scheduledTour)
    {
        return _scheduledTourRepository.Save(scheduledTour);
    }

    public void Update(ScheduledTour scheduledTour)
    {
        _scheduledTourRepository.Update(scheduledTour);
    }

    public void Delete(ScheduledTour scheduledTour)
    {
        _scheduledTourRepository.Delete(scheduledTour);
    }
}
