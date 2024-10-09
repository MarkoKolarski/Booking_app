using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;

namespace BookingApp.Repository
{
    public class SuperGuestRepository : ISuperGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/superGuest.csv";
        private readonly Serializer<SuperGuest> _serializer;
        private List<SuperGuest> _superGuests;

        public SuperGuestRepository()
        {
            _serializer = new Serializer<SuperGuest>();
            _superGuests = _serializer.FromCSV(FilePath);
        }

        public List<SuperGuest> GetAll()
        {
            _superGuests = _serializer.FromCSV(FilePath);
            return _superGuests;
        }

        public SuperGuest GetByGuestId(int guestId)
        {
            return _superGuests.FirstOrDefault(s => s.GuestId == guestId);
        }



        public SuperGuest Save(SuperGuest superGuest)
        {
            superGuest.Id = NextId();
            _superGuests = _serializer.FromCSV(FilePath);
            _superGuests.Add(superGuest);
            _serializer.ToCSV(FilePath, _superGuests);
            return superGuest;
        }



        public int NextId()
        {
            _superGuests = _serializer.FromCSV(FilePath);
            return _superGuests.Any() ? _superGuests.Max(s => s.Id) + 1 : 1;
        }



        public void Update(SuperGuest updatedSuperGuest)
        {
            _superGuests = _serializer.FromCSV(FilePath);
            SuperGuest current = _superGuests.Find(s => s.Id == updatedSuperGuest.Id);
            int index = _superGuests.IndexOf(current);
            _superGuests.Remove(current);
            _superGuests.Insert(index, updatedSuperGuest);
            _serializer.ToCSV(FilePath, _superGuests);
        }


        public void Delete(SuperGuest superGuest)
        {
            _superGuests = _serializer.FromCSV(FilePath);
            _superGuests.RemoveAll(s => s.Id == superGuest.Id);
            _serializer.ToCSV(FilePath, _superGuests);
        }
    }
}