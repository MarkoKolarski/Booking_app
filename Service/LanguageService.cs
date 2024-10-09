using System.Collections.Generic;
using BookingApp.Repository.Interface;

namespace BookingApp.Service
{
    public class LanguageService
    {
        private readonly ILanguageRepository _locationRepository;

        public LanguageService()
        {
            _locationRepository = Injector.CreateInstance<ILanguageRepository>();
        }
        public List<string> GetAll()
        {
            return _locationRepository.GetAll();
        }
    }
}
