using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepository _guestRatingRepository;

        public GuestRatingService()
        {
            _guestRatingRepository = Injector.CreateInstance<IGuestRatingRepository>();
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatingRepository.GetAll();
        }

        public GuestRating Save(GuestRating guestRating)
        {
            return _guestRatingRepository.Save(guestRating);
        }

        public int NextId()
        {
            return _guestRatingRepository.NextId();
        }

        public bool ExistsByReservationId(int reservationId)
        {
            return _guestRatingRepository.ExistsByReservationId(reservationId);
        }

        public List<GuestRating> GetByOwnerId(int ownerId)
        {
            return _guestRatingRepository.GetByOwnerId(ownerId);
        }

        public IEnumerable<IGrouping<int, GuestRating>> GetRatingsGroupedByGuestId(int ownerId)
        {
            return _guestRatingRepository.GetRatingsGroupedByGuestId(ownerId);
        }
        public bool ExistByGuestAndOwnerId(int guestId, int ownerId)
        {
            return _guestRatingRepository.ExistByGuestAndOwnerId(guestId, ownerId);
        }
    }
}
