using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class SuperOwner : ISerializable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public bool IsSuperOwner { get; set; }
        public int TotalRatings { get; set; }
        public float AverageRating { get; set; }
        public SuperOwner() { }

        public SuperOwner(int ownerId, bool isSuperOwner, int totalRatings, float averageRating)
        {
            OwnerId = ownerId;
            IsSuperOwner = isSuperOwner;
            TotalRatings = totalRatings;
            AverageRating = averageRating;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            IsSuperOwner = Convert.ToBoolean(values[2]);
            TotalRatings = Convert.ToInt32(values[3]);
            AverageRating = Convert.ToSingle(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), OwnerId.ToString(), IsSuperOwner.ToString(), TotalRatings.ToString(), AverageRating.ToString() };
            return csvValues;
        }
    }
}