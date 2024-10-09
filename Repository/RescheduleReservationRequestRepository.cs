using BookingApp.Model;
using BookingApp.Repository.Interface;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class RescheduleReservationRequestRepository : IRescheduleReservationRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/rescheduleReservationRequests.csv";
        private readonly Serializer<RescheduleReservationRequest> _serializer;
        private List<RescheduleReservationRequest> _rescheduleReservationsRequest;
        private readonly ReservationRepository _reservationRepository;

        public RescheduleReservationRequestRepository()
        {
            _serializer = new Serializer<RescheduleReservationRequest>();
            _rescheduleReservationsRequest = new List<RescheduleReservationRequest>();
            _reservationRepository = new ReservationRepository();
            LoadRescheduleReservationRequests();
        }

        private void LoadRescheduleReservationRequests()
        {
            _rescheduleReservationsRequest = _serializer.FromCSV(FilePath);
        }

        public List<RescheduleReservationRequest> GetAll()
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest;
        }

        public RescheduleReservationRequest Save(RescheduleReservationRequest rescheduleReservation)
        {
            LoadRescheduleReservationRequests();
            rescheduleReservation.Id = NextId();
            _rescheduleReservationsRequest.Add(rescheduleReservation);
            SaveRescheduleReservations();
            return rescheduleReservation;
        }

        private void SaveRescheduleReservations()
        {
            _serializer.ToCSV(FilePath, _rescheduleReservationsRequest);
        }

        public int NextId()
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest.Count < 1 ? 1 : _rescheduleReservationsRequest.Max(r => r.Id) + 1;
        }

        public bool ExistsByReservationId(int reservationId)
        {
            return _rescheduleReservationsRequest.Exists(r => r.Reservation.Id == reservationId);
        }

        public List<RescheduleReservationRequest> FindByGuestId(int guestId)
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest.FindAll(r => r.Reservation.Guest.Id == guestId);
        }

        public List<RescheduleReservationRequest> GetByOwnerId(int ownerId)
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest.FindAll(r => r.Reservation.Accommodation.OwnerId == ownerId && r.Status == RequestStatus.Neodlučeno);
        }

        public RescheduleReservationRequest Update(RescheduleReservationRequest rescheduleReservation)
        {
            LoadRescheduleReservationRequests();
            RescheduleReservationRequest current = _rescheduleReservationsRequest.Find(r => r.Id == rescheduleReservation.Id);
            int index = _rescheduleReservationsRequest.IndexOf(current);
            _rescheduleReservationsRequest.Remove(current);
            _rescheduleReservationsRequest.Insert(index, rescheduleReservation);
            SaveRescheduleReservations();
            return rescheduleReservation;
        }
        public void Delete(RescheduleReservationRequest rescheduleReservation)
        {
            LoadRescheduleReservationRequests();
            RescheduleReservationRequest current = _rescheduleReservationsRequest.Find(r => r.Id == rescheduleReservation.Id);
            _rescheduleReservationsRequest.Remove(current);
            SaveRescheduleReservations();
        }

        public RescheduleReservationRequest GetById(int id)
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest.Find(r => r.Id == id);
        }
        public int CountAllByYearAndAccommodation(int year, int accommodationId)
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest.Count(r => r.Reservation.Accommodation.Id == accommodationId && r.Reservation.ReservedDate.Item1.Year == year);
        }
        public int CountPostpondedByMonthAndAccommodation(int month, int year, int accommodationId)
        {
            LoadRescheduleReservationRequests();
            return _rescheduleReservationsRequest.Count(r => r.Reservation.Accommodation.Id == accommodationId && r.Reservation.ReservedDate.Item1.Month == month && r.Reservation.ReservedDate.Item1.Year == year);
        }
    }
}
