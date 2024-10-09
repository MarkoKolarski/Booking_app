using System;
using System.Collections.Generic;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        List<TourRequest> GetAllByWaiting();
        Dictionary<string, int> GetStatisticsByLabelAndYear(string label, string year);
        (string, string) GetMostWantedLocationAndLanguage();
        TourRequest Save(TourRequest tourRequest);
        void Update(TourRequest updatedTourRequest);
        List<TourRequest> GetAllByTouristId(int touristId);
        List<string> GetItemsWithInvalidStatus<T>(int touristId, Func<TourRequest, string> selector);
    }
}
