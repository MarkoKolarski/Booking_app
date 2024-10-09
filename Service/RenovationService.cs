using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class RenovationService
    {
        private readonly IRenovationRepository _renovationRepository;

        public RenovationService()
        {
            _renovationRepository = Injector.CreateInstance<IRenovationRepository>();
        }


        public List<Renovation> GetActiveByAccommodationId(int accommodationId)
        {
            return _renovationRepository.GetActiveByAccommodationId(accommodationId);
        }


        public List<Renovation> GetAll()
        {
            return _renovationRepository.GetAll();
        }

        public Renovation SaveRenovation(Renovation renovation)
        {
            return _renovationRepository.Save(renovation);
        }

        public Renovation GetByAccommodation(int accommodationId)
        {
            return _renovationRepository.GetByAccommodation(accommodationId);
        }
        public Renovation GetById(int id)
        {
            return _renovationRepository.GetById(id);
        }
        public Renovation Update(Renovation renovation)
        {
            return _renovationRepository.Update(renovation);
        }
    }
}
