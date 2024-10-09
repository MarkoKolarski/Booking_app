using BookingApp.Serializer;
using System;


namespace BookingApp.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public int ForumId { get; set; } 
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public bool IsFromVisitor { get; set; }

        public Comment() { }

        public Comment(DateTime creationTime, string text, User user, int forumId)
        {
            CreationTime = creationTime;
            Text = text;
            User = user;
            ForumId = forumId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ForumId.ToString(), CreationTime.ToString(), Text, User.Id.ToString(), IsFromVisitor.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ForumId = Convert.ToInt32(values[1]);
            CreationTime = Convert.ToDateTime(values[2]);
            Text = values[3];
            User = new User() { Id = Convert.ToInt32(values[4]) };
            IsFromVisitor = Convert.ToBoolean(values[5]);
        }
    }

}
