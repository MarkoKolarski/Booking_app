using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.Tourist
{
    public class TourRequestsViewModel : ViewModelBase
    {
        private readonly User user;

        private readonly TourRequestService _tourRequestService = new TourRequestService();

        private TourRequest tourRequest;

        private List<TourRequest> _tourRequests;
        public List<TourRequest> TourRequests
        {
            get => _tourRequests;
            set
            {
                _tourRequests = value;
                OnPropertyChanged(nameof(_tourRequests));
            }
        }

        public TourRequestsViewModel(User user)
        {
            this.user = user;
            TourRequests = new List<TourRequest>();
            LoadTourRequests(user);

        }

        /*public void LoadTourRequests(User user)
        {
            TourRequests = GetAllTourRequests(user.Id);
        }*/

        private void LoadTourRequests(User user)
        {
            List<TourRequest> tourRequests = _tourRequestService.GetAllByTouristId(user.Id);

            foreach (var tourRequest in tourRequests)
            {
                // Provera uslova za promenu statusa ture
                DateTime timeLimit = tourRequest.Start.AddDays(-2);
                if (DateTime.Now >= timeLimit && tourRequest.Status == TourRequestStatus.Waiting)
                {
                    tourRequest.Status = TourRequestStatus.Invalid;
                    _tourRequestService.Update(tourRequest);
                }
            }

            TourRequests = tourRequests;
        }


    }
}
