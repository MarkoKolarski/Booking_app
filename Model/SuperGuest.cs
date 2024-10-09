using System;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class SuperGuest : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int ReservationCount { get; set; }
        public bool IsSuperGuest { get; set; }
        public int BonusPoints { get; set; }
        public DateTime? StartDate { get; set; }

        public SuperGuest()
        {
            ReservationCount = 0;
            IsSuperGuest = false;
            BonusPoints = 0;
            StartDate = DateTime.Now;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            ReservationCount = Convert.ToInt32(values[2]);
            IsSuperGuest = Convert.ToBoolean(values[3]);
            BonusPoints = Convert.ToInt32(values[4]);
            StartDate = ParseStartDate(values[5]);
        }

        private DateTime? ParseStartDate(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToDateTime(value);
            }
            else
            {
                return null;
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), ReservationCount.ToString(), IsSuperGuest.ToString(), BonusPoints.ToString(), StartDate?.ToString() ?? "" };
            return csvValues;
        }

        public SuperGuest AddReservation()
        {
            if ((DateTime.Now - StartDate.Value).TotalDays > 365)
            {
                ReservationCount = 1;
                StartDate = DateTime.Now;
                IsSuperGuest = false;
                BonusPoints = 0;
            }
            else
            {
                ReservationCount++;
            }

            if (ReservationCount == 10)
            {
                StartDate = DateTime.Now;
                IsSuperGuest = true;
                BonusPoints = 5;
            }

            return this;
        }


        public SuperGuest UseBonusPoint()
        {
            if (IsSuperGuest && BonusPoints > 0)
            {
                BonusPoints--;
            }
            return this;
        }

        public void ResetBonusPoints()
        {
            if (!IsSuperGuest)
            {
                BonusPoints = 0;
            }
        }


    }
}