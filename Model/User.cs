using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public enum Role
    {
        Owner,
        Guest,
        Guide,
        Tourist
    }

    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

        public Role Role { get; set; }

        public string Picture { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString(), Picture, Email };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = Enum.Parse<Role>(values[3]);
            Picture = values[4];
            Email = values[5];
        }
    }
}