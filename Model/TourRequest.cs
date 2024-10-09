using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public enum TourRequestStatus
    {
        Waiting,
        Accepted,
        Invalid
    }

    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int TouristNumber { get; set; }
        public List<Tourist> Tourists { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TourRequestStatus Status { get; set; }

        public TourRequest() { }

        public TourRequest(string location, string description, string language, int touristNumber, List<Tourist> tourists, DateTime start, DateTime end, TourRequestStatus status)
        {
            Location = location;
            Description = description;
            Language = language;
            TouristNumber = touristNumber;
            Tourists = tourists;
            Start = start;
            End = end;
            Status = status;
        }

        public static (int, int, int, double, double, double) CalculateStatistics(List<TourRequest> requests)
        {
            int totalRequests = requests.Count;
            int acceptedRequests = requests.Count(tr => tr.Status == TourRequestStatus.Accepted);
            int rejectedRequests = requests.Count(tr => tr.Status == TourRequestStatus.Invalid);
            double acceptedPercentage = totalRequests > 0 ? (double)acceptedRequests / totalRequests * 100 : 0;
            double rejectedPercentage = totalRequests > 0 ? (double)rejectedRequests / totalRequests * 100 : 0;

            int totalParticipantsInAcceptedRequests = requests.Where(tr => tr.Status == TourRequestStatus.Accepted).Sum(tr => tr.TouristNumber);

            double averageParticipantsInAcceptedRequests = acceptedRequests > 0 ? (double)totalParticipantsInAcceptedRequests / acceptedRequests : 0;

            return (totalRequests, acceptedRequests, rejectedRequests, acceptedPercentage, rejectedPercentage, averageParticipantsInAcceptedRequests);
        }

        public static Dictionary<string, int> CalculateRequestByLabel(List<TourRequest> requests, string label)
        {
            var requestLabelCounts = new Dictionary<string, int>();

            foreach (var request in requests)
            {
                string key;
                if (label == "Location")
                    key = request.Location;
                else if (label == "Language")
                    key = request.Language;
                else
                    throw new ArgumentException("Invalid label.", nameof(label));

                if (!requestLabelCounts.ContainsKey(key))
                    requestLabelCounts[key] = 1;
                else
                    requestLabelCounts[key]++;
            }

            return requestLabelCounts;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                Location,
                Description,
                Language,
                TouristNumber.ToString(),
                string.Join(",", Tourists.Select(t => t.ToCSV())),
                Start.ToString((string)Application.Current.Resources["DateTimeFormat"]),
                End.ToString((string)Application.Current.Resources["DateTimeFormat"]),
                Status.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = values[1];
            Description = values[2];
            Language = values[3];
            TouristNumber = Convert.ToInt32(values[4]);
            Tourists = string.IsNullOrEmpty(values[5])
                ? new List<Tourist>()
                : values[5].Split(',').Select(Tourist.FromCSV).ToList();
            Start = DateTime.ParseExact(values[6], (string)Application.Current.Resources["DateTimeFormat"], CultureInfo.InvariantCulture);
            End= DateTime.ParseExact(values[7], (string)Application.Current.Resources["DateTimeFormat"], CultureInfo.InvariantCulture);
            Status = Enum.Parse<TourRequestStatus>(values[8]);
        }
    }
}
