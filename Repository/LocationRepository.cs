using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookingApp.Repository.Interface;

namespace BookingApp.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private List<string> _locations;

        public LocationRepository()
        {
            _locations = File.ReadLines(FilePath).ToList();
        }

        public List<string> GetAll()
        {
            _locations = File.ReadLines(FilePath).ToList();
            return _locations;
        }
    }
}
