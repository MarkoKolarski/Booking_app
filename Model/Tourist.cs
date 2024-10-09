using System;

namespace BookingApp.Model
{
    public class Tourist
    {
        public int TouristId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string KeyPointName { get; set; }
        public bool IsMarkable { get; set; }

        public Tourist() { }

        public Tourist(int touristId, string name, string surname, int age, string keyPointName, bool isMarkable)
        {
            TouristId = touristId;
            Name = name;
            Surname = surname;
            Age = age;
            KeyPointName = keyPointName;
            IsMarkable = isMarkable;
        }

        public string ToCSV()
        {
            return $"{TouristId}:{Name}:{Surname}:{Age}:{KeyPointName}:{IsMarkable}";
        }

        public static Tourist FromCSV(string value)
        {
            string[] values = value.Split(':');
            return new Tourist(Convert.ToInt32(values[0]), values[1], values[2], Convert.ToInt32(values[3]),
                                               values[4], Convert.ToBoolean(values[5]));
        }
    }
}
