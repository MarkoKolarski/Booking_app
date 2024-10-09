using System.Collections.ObjectModel;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;

namespace BookingApp.ViewModel.Guide
{
    public class TourReviewsViewModel : ViewModelBase
    {
        public ObservableCollection<TourDto> _tourDto { get; set; }

        public ObservableCollection<TourReview> TourReviews { get; set; }

        private int _selectedReviewIndex = -1;
        public int SelectedReviewIndex
        {
            get => _selectedReviewIndex;
            set
            {
                _selectedReviewIndex = value;
                OnPropertyChanged(nameof(_selectedReviewIndex));
            }
        }

        private TourReviewService _tourReviewService = new TourReviewService();

        public TourReviewsViewModel(TourDto tourDto)
        {
            _tourDto = new ObservableCollection<TourDto> { tourDto };
            TourReviews = new ObservableCollection<TourReview>(_tourReviewService.GetAllByScheduledTourId(tourDto.ScheduledTour.Id));
        }

        public void ReportReview(TourReview tourReview)
        {
            tourReview.IsValid = false;
            _tourReviewService.Update(tourReview);
            int index = TourReviews.IndexOf(tourReview);
            TourReviews.RemoveAt(index);
            //TourReviews.Insert(index, tourReview);
        }
    }
}
