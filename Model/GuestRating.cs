using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public int OwnerID { get; set; }

        public int GuestId { get; set; }

        public int CleanlinessRating { get; set; }

        public int RuleAdherenceRating { get; set; }

        public string AdditionalComment { get; set; } = String.Empty;

        public DateTime RatingDate { get; set; }

        public bool Rated { get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            OwnerID = Convert.ToInt32(values[2]);
            GuestId = Convert.ToInt32(values[3]);
            CleanlinessRating = Convert.ToInt32(values[4]);
            RuleAdherenceRating = Convert.ToInt32(values[5]);
            AdditionalComment = values[6];
            RatingDate = DateTime.Parse(values[7]);
            Rated = Convert.ToBoolean(values[8]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), OwnerID.ToString(), GuestId.ToString(), CleanlinessRating.ToString(), RuleAdherenceRating.ToString(), AdditionalComment, RatingDate.ToString(), Rated.ToString() };
            return csvValues;
        }
        public GuestRating() { }
        public GuestRating(int reservationId, int ownerId, int guestId, int cleanlinessRating, int ruleAdherenceRating, string additionalComment, DateTime ratingDate, bool rated)
        {
            ReservationId = reservationId;
            OwnerID = ownerId;
            GuestId = guestId;
            CleanlinessRating = cleanlinessRating;
            RuleAdherenceRating = ruleAdherenceRating;
            AdditionalComment = additionalComment;
            RatingDate = ratingDate;
            Rated = rated;
        }

        public void Rate(int cleanlinessRating, int ruleAdherenceRating, string additionalComment)
        {
            CleanlinessRating = cleanlinessRating;
            RuleAdherenceRating = ruleAdherenceRating;
            AdditionalComment = additionalComment;
            RatingDate = DateTime.Now;
            Rated = true;
        }
    }
}
