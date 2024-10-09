using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public enum Status
    {
        Scheduled,
        Live,
        Finished
    }

    public class ScheduledTour : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int GuideId { get; set; }
        public DateTime Start { get; set; }
        public List<Tourist> Tourists { get; set; }
        public int FreeSpots { get; set; }
        public Status Status { get; set; }

        public bool IsToday => Start.Date == DateTime.Today;
        public bool IsCancellable => Start > DateTime.Now.AddHours(48);

        public ScheduledTour() { }

        public ScheduledTour(int tourId, int guideId, DateTime start, List<Tourist> tourists, int freeSpots)
        {
            TourId = tourId;
            GuideId = guideId;
            Start = start;
            Tourists = tourists;
            FreeSpots = freeSpots;
            Status = Status.Scheduled;
        }

        public (int, int, int) CalculateTouristStatistics()
        {
            return (
                Tourists.Count(t => t.Age < 18),
                Tourists.Count(t => t.Age >= 18 && t.Age <= 50),
                Tourists.Count(t => t.Age > 50)
            );
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                TourId.ToString(),
                GuideId.ToString(),
                Start.ToString((string)Application.Current.Resources["DateTimeFormat"]),
                string.Join(",", Tourists.Select(t => t.ToCSV())),
                FreeSpots.ToString(),
                Status.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            Start = DateTime.ParseExact(values[3], (string)Application.Current.Resources["DateTimeFormat"], CultureInfo.InvariantCulture);
            Tourists = string.IsNullOrEmpty(values[4])
                ? new List<Tourist>()
                : values[4].Split(',').Select(Tourist.FromCSV).ToList();
            FreeSpots = Convert.ToInt32(values[5]);
            Status = Enum.Parse<Status>(values[6]);
        }
    }
}
