using BookingApp.Repository;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;

public class Injector
{
    private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
    {
        { typeof(IAccommodationRatingRepository), new AccommodationRatingRepository() },
        { typeof(IAccommodationRepository), new AccommodationRepository() },
        { typeof(IReservationRepository), new ReservationRepository() },
        { typeof(IGuestRatingRepository), new GuestRatingRepository() },
        { typeof(ISuperOwnerRepository), new SuperOwnerRepository() },
        { typeof(IRescheduleReservationRequestRepository), new RescheduleReservationRequestRepository() },
        { typeof(ILocationRepository), new LocationRepository() },
        { typeof(ILanguageRepository), new LanguageRepository() },
        { typeof(ITourRepository), new TourRepository() },
        { typeof(IScheduledTourRepository), new ScheduledTourRepository() },
        { typeof(ITourReviewRepository), new TourReviewRepository() },
        { typeof(IVoucherRepository), new VoucherRepository() },
        {typeof(IRenovationRepository), new RenovationRepository() },
        { typeof(IUserRepository), new UserRepository()},
        { typeof(ISuperGuestRepository), new SuperGuestRepository()},
        { typeof(ITourRequestRepository), new TourRequestRepository() },
        { typeof(IForumRepository), new ForumRepository() },
        { typeof(ICommentRepository), new CommentRepository() },

    };

    public static T CreateInstance<T>()
    {
        Type type = typeof(T);

        if (_implementations.ContainsKey(type))
        {
            return (T)_implementations[type];
        }

        throw new ArgumentException($"No implementation found for type {type}");
    }
}
