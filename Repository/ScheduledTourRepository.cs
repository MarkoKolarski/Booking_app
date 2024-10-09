using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class ScheduledTourRepository : IScheduledTourRepository
    {
        private const string FilePath = "../../../Resources/Data/scheduledTours.csv";

        private const string FilePathJoinedTourists = "../../../Resources/Data/notificationJoinedTourists.csv";

        private readonly Serializer<ScheduledTour> _serializer;

        private List<ScheduledTour> _scheduledTours;

        private ScheduledTour _scheduledTour;

        public ScheduledTourRepository()
        {
            _serializer = new Serializer<ScheduledTour>();
            _scheduledTours = _serializer.FromCSV(FilePath);
        }

        public List<ScheduledTour> GetAll()
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours;
        }

        public List<ScheduledTour> GetAllByStatusAndGuideId(Status status, int guideId)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours.Where(st => st.GuideId == guideId && st.Status == status)
                .OrderBy(st => st.Start)
                .ToList();
        }

        public List<ScheduledTour> GetAllByStatusAndTouristId(Status status, int touristId)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours.Where(st => st.Tourists.Any(t => t.TouristId == touristId) && st.Status == status)
                .OrderBy(st => st.Start)
                .ToList();
        }

        public List<ScheduledTour> GetOtherScheduledTours(List<Tour> otherTours, ScheduledTour scheduledTour)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours.Where(st => otherTours
                .Any(t => t.Id == st.TourId) && st.Id != scheduledTour.Id && st.FreeSpots > 0)
                .ToList();
        }

        public ScheduledTour GetMostVisitedByYear(int guideId, string year)
        {
            var finishedTours = year == "Za sva vremena"
                ? GetAllByStatusAndGuideId(Status.Finished, guideId)
                : GetAllByStatusAndGuideId(Status.Finished, guideId)
                    .Where(t => t.Start.Year.ToString() == year)
                    .ToList();

            return finishedTours.OrderByDescending(t => t.Tourists.Count)
                .FirstOrDefault();
        }

        public Tourist GetTouristById(int touristId)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours.SelectMany(st => st.Tourists)
                .FirstOrDefault(t => t.TouristId == touristId);
        }

        public ScheduledTour GetById(int id)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours.FirstOrDefault(st => st.Id == id);
        }

        public List<Tourist> GetAllTouristsByIsMarkable(ScheduledTour scheduledTour)
        {             
            _scheduledTour = GetById(scheduledTour.Id);
            return _scheduledTour.Tourists.Where(t => t.IsMarkable).ToList();
        }

        public void NotifyJoinedTourist(ScheduledTour scheduledTour)
        {
            List<Tourist> tourists = GetAllTouristsByIsMarkable(scheduledTour);
            WriteTouristsToCSV(tourists);
        }

        private void WriteTouristsToCSV(List<Tourist> tourists)
        {
            List<string> lines = new List<string>();
            foreach (Tourist tourist in tourists)
            {
                lines.Add(tourist.ToCSV());
            }
            System.IO.File.WriteAllLines(FilePathJoinedTourists, lines);
        }

        public ScheduledTour Save(ScheduledTour scheduledTour)
        {
            scheduledTour.Id = NextId();
            _scheduledTours = _serializer.FromCSV(FilePath);
            _scheduledTours.Add(scheduledTour);
            _serializer.ToCSV(FilePath, _scheduledTours);
            return scheduledTour;
        }

        public int NextId()
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            return _scheduledTours.Any() ? _scheduledTours.Max(c => c.Id) + 1 : 1;
        }

        public void Update(ScheduledTour updatedScheduledTour)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            int index = _scheduledTours.FindIndex(st => st.Id == updatedScheduledTour.Id);
            if (index != -1)
            {
                _scheduledTours[index] = updatedScheduledTour;
                _serializer.ToCSV(FilePath, _scheduledTours);
            }
        }

        public void Delete(ScheduledTour scheduledTour)
        {
            _scheduledTours = _serializer.FromCSV(FilePath);
            _scheduledTours.RemoveAll(st => st.Id == scheduledTour.Id);
            _serializer.ToCSV(FilePath, _scheduledTours);
        }
    }
}
