using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.Owner
{
    public class OwnerProfileViewModel : ViewModelBase
    {
        private readonly SuperOwnerService _superOwnerService;
        private readonly AccommodationRatingService _accommodationRatingService;
        public User _currentUser;
        public OwnerDto _superOwner;

        public OwnerProfileViewModel(User user)
        {
            _superOwnerService = new SuperOwnerService();
            _accommodationRatingService = new AccommodationRatingService();
            _currentUser = user;
            UpdateOrSaveSuperOwner();
            _superOwner = EntityToDto(_superOwnerService.GetByOwnerId(_currentUser.Id));
        }

        private SuperOwner CreateSuperOwner()
        {
            int totalRatings = _accommodationRatingService.CountByOwner(_currentUser.Id);
            float averageRating = (float)_accommodationRatingService.GetAverageRatingByOwner(_currentUser.Id);
            bool isSuperOwner = totalRatings >= 5 && averageRating >= 4.5;
            return new SuperOwner
            {
                OwnerId = _currentUser.Id,
                TotalRatings = totalRatings,
                AverageRating = averageRating,
                IsSuperOwner = isSuperOwner
            };
        }
        public void UpdateOrSaveSuperOwner()
        {
            SuperOwner owner = _superOwnerService.GetByOwnerId(_currentUser.Id);
            if (owner == null)
            {
                _superOwnerService.Save(CreateSuperOwner());
            }
            else
            {
                _superOwnerService.Update(CreateSuperOwner());
            }
        }
        private OwnerDto EntityToDto(SuperOwner superOwner)
        {
            return new OwnerDto(superOwner.IsSuperOwner, superOwner.TotalRatings, superOwner.AverageRating);
        }
    }
}
