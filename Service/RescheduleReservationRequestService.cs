using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class RescheduleReservationRequestService
    {
        private readonly IRescheduleReservationRequestRepository _rescheduleReservationRequestRepository;

        public RescheduleReservationRequestService()
        {
            _rescheduleReservationRequestRepository = Injector.CreateInstance<IRescheduleReservationRequestRepository>();
        }

        public List<RescheduleReservationRequest> GetAll()
        {
            return _rescheduleReservationRequestRepository.GetAll();
        }

        public List<RescheduleReservationRequest> FindByGuestId(int guestId)
        {
            return _rescheduleReservationRequestRepository.FindByGuestId(guestId);
        }

        public List<RescheduleReservationRequest> GetByOwnerId(int ownerId)
        {
            return _rescheduleReservationRequestRepository.GetByOwnerId(ownerId);
        }

        public RescheduleReservationRequest Save(RescheduleReservationRequest rescheduleReservationRequest)
        {
            return _rescheduleReservationRequestRepository.Save(rescheduleReservationRequest);
        }

        public RescheduleReservationRequest Update(RescheduleReservationRequest rescheduleReservationRequest)
        {
            return _rescheduleReservationRequestRepository.Update(rescheduleReservationRequest);
        }
        public RescheduleReservationRequest GetById(int id)
        {
            return _rescheduleReservationRequestRepository.GetById(id);
        }
        public int CountAllByYearAndAccommodation(int year, int accommodationId)
        {
            return _rescheduleReservationRequestRepository.CountAllByYearAndAccommodation(year, accommodationId);
        }
        public int CountPostpondedByMonthAndAccommodation(int month, int year, int accommodationId)
        {
            return _rescheduleReservationRequestRepository.CountPostpondedByMonthAndAccommodation(month, year, accommodationId);
        }
    }
}
