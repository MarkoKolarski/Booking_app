using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum RequestStatus
    {
        Neodlučeno,
        Odobreno,
        Odbijeno
    }
    public class RescheduleReservationRequest : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public Reservation Reservation { get; set; }
        public RequestStatus Status { get; set; }
        public string? OwnerResponse { get; set; }
        public Tuple<DateTime, DateTime>? NewReservedDate { get; set; }
        public bool IsRead { get; set; }

        public void FromCSV(string[] values)
        {
            ReservationRepository _reservationRepository = new ReservationRepository();
            UserRepository _userRepository = new UserRepository();
            Id = Convert.ToInt32(values[0]);
            Guest = _userRepository.FindById(Convert.ToInt32(values[1]));
            Reservation = _reservationRepository.GetById(Convert.ToInt32(values[2]));
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[3]);
            OwnerResponse = values[4];
            ParseReservedDate(values[5]);
            IsRead = Convert.ToBoolean(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Guest.Id.ToString(), Reservation.Id.ToString(), Status.ToString(), OwnerResponse ?? "", ConvertReservedDateToString() ?? "", IsRead.ToString() };
            return csvValues;
        }
        private string? ConvertReservedDateToString()
        {
            return NewReservedDate != null ? $"{NewReservedDate.Item1:MM/dd/yyyy HH:mm:ss} - {NewReservedDate.Item2:MM/dd/yyyy HH:mm:ss}" : null;
        }
        public void ParseReservedDate(string reservedDateString)
        {
            string[] dateParts = reservedDateString.Split('-');

            DateTime start, end;
            if (DateTime.TryParseExact(dateParts[0].Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out start) &&
                DateTime.TryParseExact(dateParts[1].Trim(), "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                NewReservedDate = new Tuple<DateTime, DateTime>(start, end);
            }
            else
            {

                Console.WriteLine($"Greška pri konverziji datuma iz stringa \"{reservedDateString}\".");

            }
        }
    }
}
