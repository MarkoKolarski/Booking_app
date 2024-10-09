using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class AccommodationRatingDto : ViewModelBase
    {
        private int _id;
        private int _cleanliness;
        private int _ownerCorrectness;
        private string _comment;
        private ObservableCollection<string> _cleanlinessImages;
        private ObservableCollection<string> _ownerCorrectnessImages;
        private ObservableCollection<string> _guestImages;
        private string _guestName;
        private string _accommodationName;
        public AccommodationRatingDto(int id ,int cleanliness, int ownerCorrectness, string comment, string guestName, string accommodationName, ObservableCollection<string> guestImages)
        {
            _id = id;
            _cleanliness = cleanliness;
            _ownerCorrectness = ownerCorrectness;
            _comment = comment;
            _guestName = guestName;
            _accommodationName = accommodationName;
            _guestImages = guestImages;
            UpdateCleanlinessImages();
            UpdateOwnerCorrectnessImages();
        }

        public int Id => _id;
        public int Cleanliness => _cleanliness;

        public int OwnerCorrectness => _ownerCorrectness;

        public string Comment => _comment;

        public string GuestName => _guestName;

        public string AccommodationName => _accommodationName;

        public ObservableCollection<string> CleanlinessImages => _cleanlinessImages;

        public ObservableCollection<string> OwnerCorrectnessImages => _ownerCorrectnessImages;
        public ObservableCollection<string> GuestImages => _guestImages;

        private void UpdateCleanlinessImages()
        {
            _cleanlinessImages = new ObservableCollection<string>(Enumerable.Repeat("../Resources/Images/solidStar.png", _cleanliness));
            for(int i = _cleanliness; i < 5; i++)
            {
                _cleanlinessImages.Add("/Resources/Images/emptyStar.png");
            }
        }

        private void UpdateOwnerCorrectnessImages()
        {
            _ownerCorrectnessImages = new ObservableCollection<string>(Enumerable.Repeat("/Resources/Images/solidStar.png", _ownerCorrectness));
            for (int i = _ownerCorrectness; i < 5; i++)
            {
                _ownerCorrectnessImages.Add("../Resources/Images/emptyStar.png");
            }
        }
    }
}
