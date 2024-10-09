using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace BookingApp.ViewModel.Tourist
{
    public class TourRatingViewModel : ViewModelBase
    {
        private readonly ScheduledTourService _scheduledTourService;
        private readonly TourReviewService _tourReviewService;
        private readonly User user;
        private ObservableCollection<TourDto> _tourDtos;
        private TourDto _selectedTourDto;
        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();
        private string _destinationPath = "../../../Resources/Images/";
        private string _relativePath = "../Resources/Images/";

        public delegate void CloseWindowEventHandler();
        public event CloseWindowEventHandler CloseWindow;

        public ObservableCollection<TourDto> TourDtos
        {
            get { return _tourDtos; }
            set
            {
                if (_tourDtos != value)
                {
                    _tourDtos = value;
                    OnPropertyChanged(nameof(TourDtos));
                }
            }
        }

        public TourDto SelectedTourDto
        {
            get { return _selectedTourDto; }
            set
            {
                if (_selectedTourDto != value)
                {
                    _selectedTourDto = value;
                    OnPropertyChanged(nameof(SelectedTourDto));
                }
            }
        }

        private int _knowledgeLevel = 1;
        public int KnowledgeLevel
        {
            get { return _knowledgeLevel; }
            set
            {
                if (_knowledgeLevel != value)
                {
                    _knowledgeLevel = value;
                    OnPropertyChanged(nameof(KnowledgeLevel));
                }
            }
        }

        private int _languageLevel = 1;
        public int LanguageLevel
        {
            get { return _languageLevel; }
            set
            {
                if (_languageLevel != value)
                {
                    _languageLevel = value;
                    OnPropertyChanged(nameof(LanguageLevel));
                }
            }
        }

        private int _entertainmentLevel = 1;
        public int EntertainmentLevel
        {
            get { return _entertainmentLevel; }
            set
            {
                if (_entertainmentLevel != value)
                {
                    _entertainmentLevel = value;
                    OnPropertyChanged(nameof(EntertainmentLevel));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        public TourRatingViewModel(User user)
        {
            this.user = user;
            _scheduledTourService = new ScheduledTourService();
            _tourReviewService = new TourReviewService();
            TourDtos = new ObservableCollection<TourDto>();
            ShowAllFinishedTours();
        }

        private void ShowAllFinishedTours()
        {
            List<ScheduledTour> allFinishedTours = _scheduledTourService.GetAllByStatusAndTouristId(Status.Finished, user.Id);
            TourService tourService = new TourService();
            foreach (ScheduledTour finishedTour in allFinishedTours)
            {
                Tour tour = tourService.GetById(finishedTour.TourId);
                if (tour != null)
                {
                    TourDto tourDto = new TourDto(tour, finishedTour);
                    TourDtos.Add(tourDto);
                }
            }
        }

        public void UploadImages()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    string destinationPath = Path.Combine(_destinationPath, Path.GetFileName(fileName));
                    File.Copy(fileName, destinationPath, overwrite: true);
                    string relativePath = $"{_relativePath}{Path.GetFileName(fileName)}";
                    string absolutePath = Path.GetFullPath(destinationPath);

                    if (!Images.Contains(absolutePath))
                    {
                        Images.Add(absolutePath);
                    }
                }
            }
        }

        public void SubmitRating()
        {
            if (SelectedTourDto == null)
            {
                MessageBox.Show("Please select a tour before rating.");
                return;
            }

            if (SelectedTourDto != null)
            {
                Model.Tourist tourist = _scheduledTourService.GetTouristById(user.Id);
                TourReview review = new TourReview(SelectedTourDto.ScheduledTour.Id, tourist, KnowledgeLevel, LanguageLevel, EntertainmentLevel, Comment, Images.ToList(), false);
                _tourReviewService.Save(review);

                ResetRatingFields();
                MessageBox.Show("Rating submitted successfully!");
                CloseWindow?.Invoke();
            }
        }

        private void ResetRatingFields()
        {
            KnowledgeLevel = 0;
            LanguageLevel = 0;
            EntertainmentLevel = 0;
            Comment = string.Empty;
            SelectedTourDto = null;
        }
    }
}
