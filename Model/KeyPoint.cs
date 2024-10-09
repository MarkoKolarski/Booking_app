using System;

namespace BookingApp.Model
{
    public class KeyPoint
    {
        public string Name { get; set; }
        public bool IsMarked { get; set; }

        public KeyPoint() { }

        public KeyPoint(string name, bool isMarked)
        {
            Name = name;
            IsMarked = isMarked;
        }

        public string ToCSV()
        {
            return $"{Name}:{IsMarked}";
        }

        public static KeyPoint FromCSV(string value)
        {
            string[] values = value.Split(':');
            return new KeyPoint(values[0], Convert.ToBoolean(values[1]));
        }
    }
}
