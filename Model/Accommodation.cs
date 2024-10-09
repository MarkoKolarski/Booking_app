using BookingApp.Serializer;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookingApp.Model
{

    public enum AccommodationType
    {
        Apartment,
        House,
        Cabin,

    }

    public class Accommodation : ISerializable
    {

        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public (string City, string Country) Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancellationDays { get; set; }
        public List<string> Pictures { get; set; }
        public List<Reservation> Reservations { get; set; }


        public Accommodation()
        {
            // Default constructor to suppress warnings
            Name = string.Empty;
            Pictures = new List<string>();
        }



        public Accommodation(string name,int ownerId, (string City, string Country) location, AccommodationType type, int minReservationDays, List<string> pictures, int maxGuests, int cancellationDays = 1)
        {
            Name = name;
            OwnerId = ownerId;
            Location = location;
            Type = type;
            MinReservationDays = minReservationDays;
            CancellationDays = cancellationDays;
            MaxGuests = maxGuests;
            Pictures = pictures;
        }


        public string[] ToCSV()
        {

            string[] csvValues = { Id.ToString(), OwnerId.ToString(), Name, $"{Location.City},{Location.Country}", Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancellationDays.ToString(), string.Join("|", Pictures) };
            return csvValues;
        }

        public void FromCSV(string[] csvValues)
        {
            Id = Convert.ToInt32(csvValues[0]);
            OwnerId = Convert.ToInt32(csvValues[1]);
            Name = csvValues[2];
            Location = (csvValues[3].Split(',')[0], csvValues[3].Split(',')[1]);
            Type = Enum.Parse<AccommodationType>(csvValues[4]);
            MaxGuests = Convert.ToInt32(csvValues[5]);
            MinReservationDays = Convert.ToInt32(csvValues[6]);
            CancellationDays = Convert.ToInt32(csvValues[7]);
            Pictures = LoadPicturesFromCSV(csvValues[8]);
        }

        private static List<string> LoadPicturesFromCSV(string pictureString)
        {
            return pictureString.Split(',')
                .Where(picture => !string.IsNullOrWhiteSpace(picture))
                .Select(picture => picture.Trim())
                .ToList();
        }

    }
}
