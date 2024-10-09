using BookingApp.Model;

namespace BookingApp.Dto
{
    public class TourDto
    {
        public Tour Tour { get; set; }
        public ScheduledTour ScheduledTour { get; set; }

        public TourDto() { }

        public TourDto(Tour tour, ScheduledTour scheduledTour)
        {
            Tour = tour;
            ScheduledTour = scheduledTour;
        }

    }
}
