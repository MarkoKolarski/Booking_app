using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public string  Location { get; set; }
        public string Title { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Creator { get; set; }
        public bool IsClosed { get; set; }
        public bool isVeryUseful { get; set; }

        public Forum()
        {
            Comments = new List<Comment>();
        }

        public void Close()
        {
            IsClosed = true;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Location, Title, Creator.Id.ToString(), CreatedAt.ToString(), IsClosed.ToString(), isVeryUseful.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Location = values[1];
            Title = values[2];
            Creator = new User() { Id = Convert.ToInt32(values[3]) };
            CreatedAt = DateTime.Parse(values[4]);
            IsClosed = Convert.ToBoolean(values[5]);
            isVeryUseful = Convert.ToBoolean(values[6]);
        }
    }
}