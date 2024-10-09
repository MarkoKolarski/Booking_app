using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookingApp.Repository.Interface;

namespace BookingApp.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.csv";

        private List<string> _languages;

        public LanguageRepository()
        {
            _languages = File.ReadLines(FilePath).ToList();
        }

        public List<string> GetAll()
        {
            _languages = File.ReadLines(FilePath).ToList();
            return _languages;
        }
    }
}
