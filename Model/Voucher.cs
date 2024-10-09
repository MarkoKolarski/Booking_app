using System;
using System.Globalization;
using System.Windows;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public DateTime Expiration { get; set; }

        public Voucher() { } 

        public Voucher(int touristId, DateTime expiration)
        {
            TouristId = touristId;
            Expiration = expiration;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                TouristId.ToString(),
                Expiration.ToString((string)Application.Current.Resources["DateTimeFormat"], CultureInfo.InvariantCulture)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            Expiration = DateTime.ParseExact(values[2], (string)Application.Current.Resources["DateTimeFormat"], CultureInfo.InvariantCulture);
        }
    }
}
