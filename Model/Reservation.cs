using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BookingApp.Model
{
    public class Reservation : ISerializable
    {
        public int Id { get; set; }
        public User? Guest { get; set; }
        public Accommodation? Accommodation { get; set; }
        public Tuple<DateTime, DateTime>? ReservedDate { get; set; }
        public int NumberOfGuests { get; set; }
        public AccommodationRating? AccommodationRating { get; set; }
        public bool Deleted { get; set; }

        public void FromCSV(string[] values)
        {
            UserRepository userRepository = new UserRepository();
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            Id = Convert.ToInt32(values[0]);
            Guest = userRepository.FindById(Convert.ToInt32(values[1]));
            Accommodation = accommodationRepository.FindById(Convert.ToInt32(values[2]));
            ParseReservedDate(values[3]);
            NumberOfGuests = Convert.ToInt32(values[4]);
            AccommodationRating = ParseAccommodationRating(values[5]);
            Deleted = Convert.ToBoolean(values[6]);
        }

        private AccommodationRating? ParseAccommodationRating(string value)
        {
            AccommodationRatingRepository accommodationRatingRepository = new AccommodationRatingRepository();
            if (!string.IsNullOrEmpty(value))
            {
                return accommodationRatingRepository.FindById(Convert.ToInt32(value));
            }
            else
            {
                return null;
            }
        }

        public Reservation()
        {
            Guest = new User();
            ReservedDate = new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now);
            Deleted = false;

        }

        public Reservation(User guest, Accommodation accommodation, Tuple<DateTime, DateTime> reservedDate)
        {
            Guest = guest;
            Accommodation = accommodation;
            ReservedDate = reservedDate;
        }

        public string[] ToCSV()
        {

            List<string> csvValues = new List<string> { Id.ToString(), Guest.Id.ToString() ?? "", Accommodation?.Id.ToString() ?? "", ConvertReservedDateToString() ?? "", NumberOfGuests.ToString(), AccommodationRating?.Id.ToString() ?? "", Deleted.ToString() };
            return csvValues.ToArray();
        }
        private string? ConvertReservedDateToString()
        {
            return ReservedDate != null ? $"{ReservedDate.Item1:MM/dd/yyyy HH:mm:ss} - {ReservedDate.Item2:MM/dd/yyyy HH:mm:ss}" : null;
        }

        public void ParseReservedDate(string reservedDateString)
        {
            string[] dateParts = reservedDateString.Split('-');

            DateTime start, end;
            if (DateTime.TryParseExact(dateParts[0].Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out start) &&
                DateTime.TryParseExact(dateParts[1].Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ReservedDate = new Tuple<DateTime, DateTime>(start, end);
            }
            else
            {

                Console.WriteLine($"Greška pri konverziji datuma iz stringa \"{reservedDateString}\".");

            }
        }

    }
}