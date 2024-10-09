using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    public interface IRescheduleReservationRequestRepository
    {
        List<RescheduleReservationRequest> GetAll();
        RescheduleReservationRequest Save(RescheduleReservationRequest rescheduleReservation);
        int NextId();
        bool ExistsByReservationId(int reservationId);
        List<RescheduleReservationRequest> FindByGuestId(int guestId);
        List<RescheduleReservationRequest> GetByOwnerId(int ownerId);
        public RescheduleReservationRequest Update(RescheduleReservationRequest rescheduleReservation);

        void Delete(RescheduleReservationRequest rescheduleReservation);
        public RescheduleReservationRequest GetById(int id);
        int CountAllByYearAndAccommodation(int year, int accommodationId);
        int CountPostpondedByMonthAndAccommodation(int month, int year, int accommodationId);
    }
}
