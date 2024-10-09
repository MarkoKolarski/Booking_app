using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class TourReview : ISerializable
    {
        public int Id { get; set; }
        public int FinishedTourId { get; set; }
        public Tourist Tourist { get; set; }
        public int KnowledgeLevel { get; set; }
        public int LanguageLevel { get; set; }
        public int EntertainmentLevel { get; set; }
        public string Comment { get; set; }
        public List<string> Images { get; set; }
        public bool IsValid { get; set; }

        public TourReview() { }

        public TourReview(int finishedTourId, Tourist tourist, int knowledgeLevel, int languageLevel, int entertainmentLevel, string comment, List<string> images, bool isValid)
        {
            FinishedTourId = finishedTourId;
            Tourist = tourist;
            KnowledgeLevel = knowledgeLevel;
            LanguageLevel = languageLevel;
            EntertainmentLevel = entertainmentLevel;
            Comment = comment;
            Images = images;
            IsValid = isValid;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Id.ToString(),
                FinishedTourId.ToString(),
                Tourist.ToCSV(),
                KnowledgeLevel.ToString(),
                LanguageLevel.ToString(),
                EntertainmentLevel.ToString(),
                Comment,
                string.Join(",", Images),
                IsValid.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FinishedTourId = Convert.ToInt32(values[1]);
            Tourist = Tourist.FromCSV(values[2]);
            KnowledgeLevel = Convert.ToInt32(values[3]);
            LanguageLevel = Convert.ToInt32(values[4]);
            EntertainmentLevel = Convert.ToInt32(values[5]);
            Comment = values[6];
            Images = values[7].Split(',').ToList();
            IsValid = Convert.ToBoolean(values[8]);
        }
    }
}
