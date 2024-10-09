using BookingApp.Model;
using System.Collections.Generic;
using BookingApp.Repository.Interface;
using System;

namespace BookingApp.Service
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository _tourRequestRepository;

        public TourRequestService()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public List<TourRequest> GetAllByWaiting()
        {
            return _tourRequestRepository.GetAllByWaiting();
        }

        public Dictionary<string, int> GetStatisticsByLabelAndYear(string label, string year)
        {
            return _tourRequestRepository.GetStatisticsByLabelAndYear(label, year);
        }

        public (string, string) GetMostWantedLocationAndLanguage()
        {
            return _tourRequestRepository.GetMostWantedLocationAndLanguage();
        }

        public List<TourRequest> GetAllByTouristId(int touristId)
        {
            return _tourRequestRepository.GetAllByTouristId(touristId);
        }

        public List<string> GetInvalidLocationsAndLanguages(int touristId)
        {
            List<string> invalidLocationsLanguages = _tourRequestRepository.GetItemsWithInvalidStatus<TourRequest>(touristId, tr => tr.Location);
            invalidLocationsLanguages.AddRange(_tourRequestRepository.GetItemsWithInvalidStatus<TourRequest>(touristId, tr => tr.Language));

            return invalidLocationsLanguages;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public void Update(TourRequest updatedTourRequest)
        {
            _tourRequestRepository.Update(updatedTourRequest);
        }
    }
}
