using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class RenovationRepository : IRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";
        private readonly Serializer<Renovation> _renovationSerializer;

        private List<Renovation> _renovations;

        public RenovationRepository()
        {
            _renovationSerializer = new Serializer<Renovation>();
            _renovations = new List<Renovation>();
            LoadRenovations();
        }

        public List<Renovation> GetActiveByAccommodationId(int accommodationId)
        {
            LoadRenovations();
            return _renovations.FindAll(r => r.Accommodation.Id == accommodationId && r.Status == RenovationStatus.Active);
        }

        public List<Renovation> GetAll()
        {
            LoadRenovations();
            return _renovations;
        }

        public Renovation GetById(int id)
        {
            LoadRenovations();
            return _renovations.Find(r => r.Id == id) ?? throw new ArgumentException("Renovation not found");
        }

        public Renovation Save(Renovation renovation)
        {
            LoadRenovations();
            if (renovation.Id == 0)
            {
                renovation.Id = NextId();
                _renovations.Add(renovation);
            }
            else
            {
                var index = _renovations.FindIndex(r => r.Id == renovation.Id);
                _renovations[index] = renovation;
            }
            _renovationSerializer.ToCSV(FilePath, _renovations);
            return renovation;
        }

        public int NextId()
        {
            LoadRenovations();
            return _renovations.Count == 0 ? 1 : _renovations.Max(r => r.Id) + 1;
        }

        public void LoadRenovations()
        {
            _renovations = _renovationSerializer.FromCSV(FilePath);
        }
        public Renovation GetByAccommodation(int accommodationId)
        {
            LoadRenovations();
            return _renovations.Find(r => r.Accommodation.Id == accommodationId && r.Status == RenovationStatus.Active);
        }
        public Renovation Update(Renovation renovation)
        {
            LoadRenovations();
            var index = _renovations.FindIndex(r => r.Id == renovation.Id);
            _renovations[index] = renovation;
            _renovationSerializer.ToCSV(FilePath, _renovations);
            return renovation;
        }
    }
}
