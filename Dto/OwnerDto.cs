using BookingApp.Model;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class OwnerDto : ViewModelBase
    {
        private bool _isSuperOwner { get; set; }
        private int _totalRatings { get; set; }
        private float _averageRating { get; set; }

        public bool IsSuperOwner
        {
            get => _isSuperOwner;
            set
            {
                _isSuperOwner = value;
                OnPropertyChanged(nameof(IsSuperOwner));
            }
        }

        public int TotalRatings
        {
            get => _totalRatings;
            set
            {
                _totalRatings = value;
                OnPropertyChanged(nameof(TotalRatings));
            }
        }

        public float AverageRating
        {
            get => _averageRating;
            set
            {
                _averageRating = value;
                OnPropertyChanged(nameof(AverageRating));
            }
        }

        public OwnerDto(bool isSuperOwner, int totalRatings, float averageRating)
        {
            IsSuperOwner = isSuperOwner;
            TotalRatings = totalRatings;
            AverageRating = averageRating;
        }
    }
}
