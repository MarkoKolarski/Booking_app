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
    public class GuestRatingRepository : IGuestRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/guestRatings.csv";
        private readonly Serializer<GuestRating> _serializer;
        private List<GuestRating> _guestRatings;

        public GuestRatingRepository()
        {
            _serializer = new Serializer<GuestRating>();
            _guestRatings = new List<GuestRating>();
            LoadGuestRatings();
        }

        private void LoadGuestRatings()
        {
            _guestRatings = _serializer.FromCSV(FilePath);
        }

        public List<GuestRating> GetAll()
        {
            LoadGuestRatings();
            return _guestRatings;
        }

        public GuestRating Save(GuestRating guestRating)
        {
            LoadGuestRatings();
            guestRating.Id = NextId();
            _guestRatings.Add(guestRating);
            SaveGuestRatings();
            return guestRating;
        }

        private void SaveGuestRatings()
        {
            _serializer.ToCSV(FilePath, _guestRatings);
        }

        public int NextId()
        {
            LoadGuestRatings();
            return _guestRatings.Count < 1 ? 1 : _guestRatings.Max(r => r.Id) + 1;
        }

        public bool ExistsByReservationId(int reservationId)
        {
            return _guestRatings.Exists(r => r.ReservationId == reservationId);
        }

        public List<GuestRating> GetByOwnerId(int ownerId)
        {
            LoadGuestRatings();
            return _guestRatings.FindAll(r => r.OwnerID == ownerId);
        }

        public IEnumerable<IGrouping<int, GuestRating>> GetRatingsGroupedByGuestId(int ownerId)
        {
            LoadGuestRatings();
            return _guestRatings.FindAll(r => r.OwnerID == ownerId).GroupBy(r => r.GuestId);
        }
        public bool ExistByGuestAndOwnerId(int guestId, int ownerId)
        {
            return _guestRatings.Exists(r => r.GuestId == guestId && r.OwnerID == ownerId);
        }
    }
}
