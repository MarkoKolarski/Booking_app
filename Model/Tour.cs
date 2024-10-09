using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<KeyPoint> KeyPoints { get; set; }
        public int Duration { get; set; }
        public List<string> Images { get; set; }

        public Tour() { }

        public Tour(string name, string location, string description, string language, int maxTourists, List<KeyPoint> keyPoints, int duration, List<string> images)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            KeyPoints = keyPoints;
            Duration = duration;
            Images = images;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                Name,
                Location,
                Description,
                Language,
                MaxTourists.ToString(),
                string.Join(",", KeyPoints.Select(kp => kp.ToCSV())),
                Duration.ToString(),
                string.Join(",", Images)
            };
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Location = values[2];
            Description = values[3];
            Language = values[4];
            MaxTourists = Convert.ToInt32(values[5]);
            KeyPoints = values[6].Split(',').Select(KeyPoint.FromCSV).ToList();
            Duration = Convert.ToInt32(values[7]);
            Images = values[8].Split(',').ToList();
        }
    }
}
