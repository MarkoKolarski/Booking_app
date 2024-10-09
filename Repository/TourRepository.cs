using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class TourRepository : ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private const string FilePathNewTour = "../../../Resources/Data/notificationNewTour.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public List<Tour> GetAll()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours;
        }

        public List<Tour> GetAllByScheduledTours(List<ScheduledTour> scheduledTours)
        {
            _tours = _serializer.FromCSV(FilePath);
            return scheduledTours.Select(st => _tours.FirstOrDefault(t => t.Id == st.TourId))
                .Where(t => t != null)
                .ToList();
        }

        public List<Tour> GetAllByLocation(string location)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.Where(t => t.Location
                .Equals(location, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Tour GetById(int id)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FirstOrDefault(t => t.Id == id);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.Any() ? _tours.Max(c => c.Id) + 1 : 1;
        }

        public void Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            int index = _tours.FindIndex(t => t.Id == tour.Id);
            if (index != -1)
            {
                _tours[index] = tour;
                _serializer.ToCSV(FilePath, _tours);
            }
        }

        public void NotifyNewTour(List<Tour> tours)
        {
            WriteNewToursToCSV(tours);
        }

        private void WriteNewToursToCSV(List<Tour> tours)
        {
            List<string> lines = new List<string>();
            foreach (Tour tour in tours)
            {
                string line = string.Join(",", tour.ToCSV());
                lines.Add(line);
            }
            System.IO.File.WriteAllLines(FilePathNewTour, lines);
        }
    }
}
