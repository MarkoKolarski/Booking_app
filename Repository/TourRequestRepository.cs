using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public List<TourRequest> GetAll()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests;
        }

        public List<TourRequest> GetAllByWaiting()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.Where(tr => tr.Status == TourRequestStatus.Waiting).ToList();
        }

        public Dictionary<string, int> GetStatisticsByLabelAndYear(string label, string year)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            bool isYearFilterApplied = year != "Na nivou godina";

            return _tourRequests
                .Where(tr => (tr.Location == label || tr.Language == label))
                .Where(tr => year == "Na nivou godina" || tr.Start.Year.ToString() == year)
                .GroupBy(tr => isYearFilterApplied ?
                              CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tr.Start.Month) :
                              tr.Start.Year.ToString())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public (string, string) GetMostWantedLocationAndLanguage()
        {
            return (
                _tourRequests.Where(tr => tr.Start >= DateTime.Now.AddYears(-1))
                             .GroupBy(tr => tr.Location)
                             .OrderByDescending(g => g.Count())
                             .Select(g => g.Key)
                             .FirstOrDefault(),
                _tourRequests.Where(tr => tr.Start >= DateTime.Now.AddYears(-1))
                             .GroupBy(tr => tr.Language)
                             .OrderByDescending(g => g.Count())
                             .Select(g => g.Key)
                             .FirstOrDefault()
            );
        }

        public List<TourRequest> GetAllByTouristId(int touristId)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.Where(tr => tr.Tourists.Any(t => t.TouristId == touristId)).ToList();
        }

        public List<string> GetItemsWithInvalidStatus<T>(int touristId, Func<TourRequest, string> selector)
        {
            _tourRequests = GetAllByTouristId(touristId);

            List<string> invalidItems = _tourRequests.Where(tr => tr.Status == TourRequestStatus.Invalid)
                                                      .Select(selector)
                                                      .Distinct()
                                                      .ToList();

            List<string> acceptedItems = _tourRequests.Where(tr => tr.Status == TourRequestStatus.Accepted)
                                                      .Select(selector)
                                                      .Distinct()
                                                      .ToList();

            foreach (string acceptedItem in acceptedItems)
            {
                if (invalidItems.Contains(acceptedItem))
                {
                    invalidItems.Remove(acceptedItem);
                }
            }

            return invalidItems;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests = _serializer.FromCSV(FilePath);
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            return _tourRequests.Any() ? _tourRequests.Max(c => c.Id) + 1 : 1;
        }

        public void Update(TourRequest updatedTourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            int index = _tourRequests.FindIndex(st => st.Id == updatedTourRequest.Id);
            if (index != -1)
            {
                _tourRequests[index] = updatedTourRequest;
                _serializer.ToCSV(FilePath, _tourRequests);
            }
        }
    }
}
