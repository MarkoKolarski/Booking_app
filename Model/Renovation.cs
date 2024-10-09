using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum RenovationStatus
    {
        Active,
        Rejected,
        Finished
    }
    public class Renovation : ISerializable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int Lasting { get; set; }
        public RenovationStatus Status { get; set; }   

        public Renovation()
        {
            // Default constructor to suppress warnings
            Description = string.Empty;
            Accommodation = new Accommodation();
        }

        public Renovation(int id, int ownerId, Accommodation accommodation, DateTime startDate, DateTime endDate, string description, int lasting, RenovationStatus status)
        {
            Id = id;
            OwnerId = ownerId;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Lasting = lasting;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
            Id.ToString(),
            OwnerId.ToString(),
            Accommodation.Id.ToString(),
            StartDate.ToShortDateString(),
            EndDate.ToShortDateString(),    
            Description,
            Lasting.ToString(),
            Status.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] csvValues)
        {
            AccommodationRepository accommodationRepository = new AccommodationRepository();
            Id = Convert.ToInt32(csvValues[0]);
            OwnerId = Convert.ToInt32(csvValues[1]);

            int accommodationId = Convert.ToInt32(csvValues[2]);
            Accommodation = accommodationRepository.FindById(accommodationId);

            StartDate = DateTime.Parse(csvValues[3]);
            EndDate = DateTime.Parse(csvValues[4]);
            Description = csvValues[5];
            Lasting = Convert.ToInt32(csvValues[6]);
            Status = Enum.Parse<RenovationStatus>(csvValues[7]);
        }
    }
}
