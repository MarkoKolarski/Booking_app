using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    internal interface IGuestRatingRepository
    {
        List<GuestRating> GetAll();
        GuestRating Save(GuestRating guestRating);
        int NextId();
        bool ExistsByReservationId(int reservationId);
        List<GuestRating> GetByOwnerId(int ownerId);
        IEnumerable<IGrouping<int, GuestRating>> GetRatingsGroupedByGuestId(int ownerId);
        public bool ExistByGuestAndOwnerId(int guestId, int ownerId);
    }
}
