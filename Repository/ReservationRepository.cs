using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace BookingApp.Repository
{
    internal class ReservationRepository : IReservationRepository
    {

        private const string FilePath = "../../../Resources/Data/reservations.csv";
        private readonly Serializer<Reservation> _reservationSerializer;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _reservationSerializer = new Serializer<Reservation>();
            _reservations = new List<Reservation>();
            LoadReservations();
        }

        public List<Reservation> GetReservationsByAccommodationId(int id)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == id);
        }

        public List<Reservation> GetByGuestId(int id)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Guest.Id == id);
        }

        private void LoadReservations()
        {
            _reservations = _reservationSerializer.FromCSV(FilePath);
        }


        public List<Reservation> GetAll()
        {
            LoadReservations();
            return _reservations;
        }

        public Reservation GetById(int id)
        {
            LoadReservations();
            return _reservations.Find(r => r.Id == id) ?? throw new ArgumentException("Reservation not found");
        }

        public Reservation Save(Reservation reservation)
        {
            LoadReservations();
            reservation.Id = NextId();
            _reservations.Add(reservation);
            SaveReservation();
            return reservation;
        }

        public Reservation Update(Reservation reservation)
        {
            LoadReservations();
            Reservation current = _reservations.Find(r => r.Id == reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, reservation);
            SaveReservation();
            return reservation;
        }

        private void SaveReservation()
        {
            _reservationSerializer.ToCSV(FilePath, _reservations);
        }

        public int NextId()
        {
            LoadReservations();
            return _reservations.Count < 1 ? 1 : _reservations.Max(r => r.Id) + 1;
        }

        public List<Reservation> GetLastCheckouts(int ownerId, DateTime date)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.ReservedDate.Item2.AddDays(6) > date && r.Accommodation.OwnerId == ownerId && r.ReservedDate.Item1 < date);
        }

        public bool CheckIfCanBeRated(Reservation reservation)
        {
            return reservation.ReservedDate.Item2.AddDays(5) >= DateTime.Now;
        }

        public bool CheckForUnratedReservation()
        {
            LoadReservations();
            return _reservations.Exists(r => r.ReservedDate.Item2.AddDays(5) > DateTime.Now && r.ReservedDate.Item1 < DateTime.Now);
        }

        public void Delete(Reservation reservation)
        {
            LoadReservations();

            IRescheduleReservationRequestRepository rescheduleReservationRequestRepository = new RescheduleReservationRequestRepository();

            List<RescheduleReservationRequest> rescheduleRequests = rescheduleReservationRequestRepository.FindByGuestId(reservation.Guest.Id);
            DeleteRescheduleRequests(rescheduleRequests, reservation.Id, rescheduleReservationRequestRepository);

            Reservation founded = _reservations.Find(r => r.Id == reservation.Id);
            _reservations.Remove(founded);
            SaveReservation();
        }

        private void DeleteRescheduleRequests(List<RescheduleReservationRequest> rescheduleRequests, int reservationId, IRescheduleReservationRequestRepository rescheduleReservationRequestRepository)
        {
            if (rescheduleRequests != null)
            {
                foreach (var request in rescheduleRequests)
                {
                    if (request.Reservation.Id == reservationId)
                    {
                        rescheduleReservationRequestRepository.Delete(request);
                    }
                }
            }
        }

        public Reservation GetReservationById(int id)
        {
            LoadReservations();
            return _reservations.Find(r => r.Id == id) ?? throw new ArgumentException("Reservation not found");
        }

        public int CountReservationsByAccommodation(int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId).Count;
        }

        public int CountNumberOfGuestByAccommodation(int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId).Sum(r => r.NumberOfGuests);
        }

        public IEnumerable<IGrouping<int, int>> GetNumberOfReservationsGroupedByYear(int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId).GroupBy(r => r.ReservedDate.Item1.Year).Select(r => new { Year = r.Key, Count = r.Count() }).GroupBy(r => r.Year, r => r.Count);
        }
        public List<Tuple<DateTime,DateTime>> GetReservedDatesByAccommodation(int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId).Select(r => r.ReservedDate).ToList();
        }
        
        public List<int> GetYearsByAccommodation(int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId).Select(r => r.ReservedDate.Item1.Year).Distinct().ToList();
        }
        public int CountReservationByYearAndAccommodation(int year, int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId && r.ReservedDate.Item1.Year == year).Count;
        }
        public List<int> GetMonthsByAccommodationAndYear(int accommodationId, int year)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId && r.ReservedDate.Item1.Year == year).Select(r => r.ReservedDate.Item1.Month).Distinct().ToList();
        }
        public int CountReservationsByMonthAndAccommodation(int month, int year, int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId && r.ReservedDate.Item1.Year == year && r.ReservedDate.Item1.Month == month).Count;
        }
        public int CountRejectedByYearAndAccommodation(int year, int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId && r.Deleted == true && r.ReservedDate.Item1.Year == year).Count;
        }
        public int CountRejectedByMonthAndAccommodation(int month, int year, int accommodationId)
        {
            LoadReservations();
            return _reservations.FindAll(r => r.Accommodation.Id == accommodationId && r.Deleted == true && r.ReservedDate.Item1.Year == year && r.ReservedDate.Item1.Month == month).Count;
        }
    }
}