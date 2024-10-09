using BookingApp.Model;
using BookingApp.Service;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel.Tourist
{
    public class RequestStatisticsViewModel : ViewModelBase
    {
        private readonly User user;
        private TourRequest _tourRequest;
        private readonly TourRequestService _tourRequestService = new TourRequestService();

        private List<TourRequest> _tourRequests;

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public int TotalRequests { get; set; }
        public int AcceptedRequests { get; set; }
        public int RejectedRequests { get; set; }
        public double AcceptedPercentage { get; set; }
        public double RejectedPercentage { get; set; }
        public double AverageParticipantsInAcceptedRequests { get; set; }
        public Dictionary<string, int> RequestsByLanguage { get; set; }
        public Dictionary<string, int> RequestsByLocation { get; set; }
        public SeriesCollection LanguageSeriesCollection { get; set; }
        public SeriesCollection LocationSeriesCollection { get; set; }

        private ObservableCollection<string> _languageLabels;
        public ObservableCollection<string> LanguageLabels
        {
            get { return _languageLabels; }
            set
            {
                _languageLabels = value;
                OnPropertyChanged(nameof(LanguageLabels));
            }
        }
        private ObservableCollection<string> _locationLabels;
        public ObservableCollection<string> LocationLabels
        {
            get { return _locationLabels; }
            set
            {
                _locationLabels = value;
                OnPropertyChanged(nameof(LanguageLabels));
            }
        }

        public RequestStatisticsViewModel(User user)
        {
            this.user = user;
            _tourRequests = _tourRequestService.GetAllByTouristId(user.Id);
            GetStatistics();
            LanguageLabels = new ObservableCollection<string>(RequestsByLanguage.Keys);
            LocationLabels = new ObservableCollection<string>(RequestsByLocation.Keys);
            UpdateChartLanguage();
            UpdateChartLocation();
        }

        public void GetStatistics()
        {
            var filteredRequests = _year == 0 ? _tourRequests : _tourRequests.Where(tr => tr.Start.Year == _year);
            var allRequests = _tourRequestService.GetAllByTouristId(user.Id);

            (TotalRequests, AcceptedRequests, RejectedRequests, AcceptedPercentage, RejectedPercentage, AverageParticipantsInAcceptedRequests) = TourRequest.CalculateStatistics(filteredRequests.ToList());
            RequestsByLanguage = TourRequest.CalculateRequestByLabel(allRequests.ToList(), "Language");
            RequestsByLocation = TourRequest.CalculateRequestByLabel(allRequests.ToList(), "Location");

            OnPropertyChanged(nameof(TotalRequests));
            OnPropertyChanged(nameof(AcceptedRequests));
            OnPropertyChanged(nameof(RejectedRequests));
            OnPropertyChanged(nameof(AcceptedPercentage));
            OnPropertyChanged(nameof(RejectedPercentage));
            OnPropertyChanged(nameof(AverageParticipantsInAcceptedRequests));
            OnPropertyChanged(nameof(RequestsByLanguage));
            OnPropertyChanged(nameof(RequestsByLocation));
        }

        private void UpdateChartLanguage()
        {
            LanguageSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Requests per language",
                    Values = new ChartValues<int>(RequestsByLanguage.Values),
                    DataLabels = true
                }
            };
            OnPropertyChanged(nameof(LanguageSeriesCollection));
        }

        private void UpdateChartLocation()
        {
            LocationSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Requests per location",
                    Values = new ChartValues<int>(RequestsByLocation.Values),
                    DataLabels = true
                }
            };
            OnPropertyChanged(nameof(LocationSeriesCollection));
        }

    }
}
