using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Tourist
{
    public class NewTourNotificationViewModel : ViewModelBase
    {
        private readonly User user;
        private readonly TourRequestService _tourRequestService = new TourRequestService();
        private readonly TourService _tourService = new TourService();

        public ObservableCollection<Tour> Tours { get; set; }

        public NewTourNotificationViewModel(User user)
        {
            this.user = user;
            MakeUniqeList();
            LoadToursFromCsv();
        }

        public void MakeUniqeList()
        {
            List<Tour> allTours = _tourService.GetAll();
            HashSet<Tour> suggestedTours = new HashSet<Tour>();
            List<string> invalidLocationsLanguages = _tourRequestService.GetInvalidLocationsAndLanguages(user.Id);

            foreach (string item in invalidLocationsLanguages)
            {
                foreach (Tour tour in allTours)
                {
                    if (tour.Language.Equals(item, StringComparison.OrdinalIgnoreCase) || tour.Location.Equals(item, StringComparison.OrdinalIgnoreCase))
                    {
                        suggestedTours.Add(tour);
                    }
                }
            }
            _tourService.NotifyNewTour(suggestedTours.ToList());
        }

        private void LoadToursFromCsv()
        {
            Tours = new ObservableCollection<Tour>();
            string csvFilePath = "../../../Resources/Data/notificationNewTour.csv";

            var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false
            };

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                while (csv.Read())
                {
                    string city = csv.GetField<string>(2);
                    string country = csv.GetField<string>(3);
                    Tour tour = new Tour
                    {
                        Id = csv.GetField<int>(0),
                        Name = csv.GetField<string>(1),
                        Location = city + ", "+ country,
                        Description = null,
                        Language = csv.GetField<string>(5),
                        MaxTourists = csv.GetField<int>(6),
                        KeyPoints = null,
                        Duration = 0,
                        Images = null
                    };

                    Tours.Add(tour);

                }
            }
        }

    }
}
