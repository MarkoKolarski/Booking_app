using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.Owner
{
    public class RatingsOverViewModel : ViewModelBase
    {
        public User currentUser { get; set; }
        public GuestRatingService _guestRatingService;
        public UserRepository _userRepository;
        public ObservableCollection<Tuple<string, float, float, string>> GuestAverageRatings { get; } = new ObservableCollection<Tuple<string, float, float, string>>();

        public RatingsOverViewModel(User user)
        {
            currentUser = user;
            _guestRatingService = new GuestRatingService();
            _userRepository = new UserRepository();
            GuestAverageRatings = new ObservableCollection<Tuple<string, float, float, string>>(GetAverageRatings());
        }

        public ObservableCollection<Tuple<string, float, float, string>> GetAverageRatings()
        {
            User guest;
            ObservableCollection<Tuple<string, float, float, string>> result = new ObservableCollection<Tuple<string, float, float, string>>();
            IEnumerable<IGrouping<int, GuestRating>> groupedRatings = _guestRatingService.GetRatingsGroupedByGuestId(currentUser.Id);
            foreach (var ratings in groupedRatings)
            {
                Tuple<float, float, string> averages = CalculateAverages(ratings);
                guest = _userRepository.FindById(ratings.Key + 1);
                result.Add(new Tuple<string, float, float, string>(guest.Username, averages.Item1, averages.Item2, averages.Item3));
            }
            return result;
        }

        public Tuple<float, float, string> CalculateAverages(IGrouping<int, GuestRating> ratings)
        {
            StringBuilder commentsBuilder = new StringBuilder();
            float cleanliness = 0;
            float ruleAdherence = 0;
            foreach (var rating in ratings)
            {
                cleanliness += rating.CleanlinessRating;
                ruleAdherence += rating.RuleAdherenceRating;
                commentsBuilder.Append(rating.AdditionalComment);
                commentsBuilder.Append("\n");
            }
            cleanliness /= ratings.Count();
            ruleAdherence /= ratings.Count();
            return new Tuple<float, float, string>(cleanliness, ruleAdherence, commentsBuilder.ToString());
        }
    }
}
