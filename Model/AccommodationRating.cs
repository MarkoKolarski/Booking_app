using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;


namespace BookingApp.Model
{

    public enum RenovationLevel
    {
        None,
        Level1 = 1, // Bilo bi lepo renovirati neke sitnice, ali sve funkcioniše kako treba i bez toga
        Level2 = 2, // Male zamerke na smeštaj koje kada bi se uklonile bi ga učinile savršenim
        Level3 = 3, // Nekoliko stvari koje su baš zasmetale bi trebalo renovirati
        Level4 = 4, // Ima dosta loših stvari i renoviranje je stvarno neophodno
        Level5 = 5  // Smeštaj je u jako lošem stanju i ne vredi ga uopšte iznajmljivati ukoliko se ne renovira
    }

    public class AccommodationRating : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerCorrectness { get; set; }
        public string Comment { get; set; }
        public List<string> GuestImages { get; set; }

        public RenovationLevel? RenovationLevel { get; set; }

        public User Guest { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime SuggestionDate { get; set; }
        // Default constructor to suppress warnings
        public AccommodationRating()
        {
            Cleanliness = 0;
            OwnerCorrectness = 0;
            Comment = string.Empty;
            GuestImages = new List<string>();
            Guest = new User();
        }


        public AccommodationRating(int cleanliness, int ownerCorrectness, string comment, List<string> guestImages, DateTime stayEndDate, User guest, Accommodation accommodation)
        {
            Cleanliness = cleanliness;
            OwnerCorrectness = ownerCorrectness;
            Comment = comment;
            GuestImages = guestImages;
            Guest = guest;
            Accommodation = accommodation;
        }

        public void FromCSV(string[] values)
        {
            UserRepository userRepository = new UserRepository();
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            Guest = userRepository.FindById(Convert.ToInt32(values[2]));
            Accommodation = accommodationRepository.FindById(Convert.ToInt32(values[3]));
            Cleanliness = Convert.ToInt32(values[4]);
            OwnerCorrectness = Convert.ToInt32(values[5]);
            Comment = values[6];

            RenovationLevel = ParseRenovationLevel(values[7]);

            GuestImages = values[8].Split('+').ToList();
            SuggestionDate = DateTime.Parse(values[9]);
        }

        private RenovationLevel? ParseRenovationLevel(string valueString)
        {
            if (!string.IsNullOrEmpty(valueString) && int.TryParse(valueString, out int renovationLevelValue))
            {
                return (RenovationLevel)renovationLevelValue;
            }
            else
            {
                return null;
            }
        }

        public string[] ToCSV()
        {
            string guestImagesString = string.Join("+", GuestImages);
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), Guest?.Id.ToString(), Accommodation?.Id.ToString() ,Cleanliness.ToString(), OwnerCorrectness.ToString(), Comment.ToString(), ((int?)RenovationLevel).ToString() ?? "", guestImagesString, SuggestionDate.ToShortDateString()};
            return csvValues;
        }

    }
}