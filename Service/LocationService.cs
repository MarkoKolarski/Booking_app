using System.Collections.Generic;
using BookingApp.Repository.Interface;

namespace BookingApp.Service
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }
        public List<string> GetAll()
        {
            return _locationRepository.GetAll();
        }
    }
}
