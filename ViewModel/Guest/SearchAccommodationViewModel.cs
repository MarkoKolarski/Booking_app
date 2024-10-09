using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Command;
using System.Windows.Input;

namespace BookingApp.ViewModel.Guest
{
    public class SearchAccommodationViewModel : ViewModelBase
    {
        private readonly AccommodationService _service;
        public List<Accommodation> AllAccommodations { get; set; }

        private List<Accommodation> _filteredAccommodations;
        public List<Accommodation> FilteredAccommodations
        {
            get { return _filteredAccommodations; }
            set
            {
                _filteredAccommodations = value;
                OnPropertyChanged("FilteredAccommodations");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                SearchAccommodation();
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
                SearchAccommodation();
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
                SearchAccommodation();
            }
        }

        private ComboBoxItem _selectedComboBoxItem;
        public ComboBoxItem SelectedComboBoxItem
        {
            get { return _selectedComboBoxItem; }
            set
            {
                _selectedComboBoxItem = value;
                OnPropertyChanged(nameof(SelectedComboBoxItem));
                UpdateTypeFromSelectedAccommodationType();
                UpdateTypeFromSelectedComboBoxItem();

            }
        }


        private AccommodationType? _type;
        public AccommodationType? Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private int? _maxGuests;
        public int? MaxGuests
        {
            get => _maxGuests;
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
                SearchAccommodation();
            }
        }

        private int? _minReservationDays;
        public int? MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                _minReservationDays = value;
                OnPropertyChanged(nameof(MinReservationDays));
                SearchAccommodation();
            }
        }

        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
                
            }
        }
        public ObservableCollection<AccommodationSearchDto> AccommodationSearchDtos { get; set; }
        private struct SearchParams
        {
            public string Name;
            public string City;
            public string Country;
            public AccommodationType? Type;
            public int? MaxGuests;
            public int? MinReservationDays;
        }
        public User User { get; set; }
        public ComboBox CmbType { get; set; }

        public ICommand FocusCommand { get; private set; }

        public SearchAccommodationViewModel(User user, ComboBox cmbType)
        {
            CmbType = cmbType;
            User = user;
            _service = new AccommodationService();
            AllAccommodations = _service.GetAll();
            FilteredAccommodations = new List<Accommodation>(AllAccommodations);
            SelectedAccommodation = new Accommodation();
            AccommodationSearchDtos = new ObservableCollection<AccommodationSearchDto>();
            PopulateAccommodationSearchDtos(AllAccommodations);

            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        private void UpdateTypeFromSelectedComboBoxItem()
        {
            string selectedItemContent = SelectedComboBoxItem?.Content.ToString();
            Type = Enum.TryParse<AccommodationType>(selectedItemContent, out var type) ? type : null;
        }

        public void SearchAccommodation()
        {
            FilteredAccommodations = FilterAccommodations();

            if (FilteredAccommodations.Count == 0)
            {
                return;
            }
            else
            {
                PopulateAccommodationSearchDtos(FilteredAccommodations);
            }
        }
        private void PopulateAccommodationSearchDtos(List<Accommodation> accommodations)
        {
            AccommodationSearchDtos.Clear();
            foreach (var accommodation in accommodations)
            {
                AccommodationSearchDtos.Add(new AccommodationSearchDto(accommodation));
            }
        }

        private List<Accommodation> FilterAccommodations()
        {
            var searchParams = ExtractSearchParams();

            if (!ValidateSearchParams(searchParams))
            {
                return AllAccommodations;
            }

            return FilterAccommodationsByCriteria(searchParams);
        }

        private SearchParams ExtractSearchParams()
        {
            var searchParams = new SearchParams
            {
                Name = Name,
                City = City,
                Country = Country,
                Type = Type,
                MaxGuests = MaxGuests,
                MinReservationDays = MinReservationDays
            };
            return searchParams;
        }

        private void UpdateTypeFromSelectedAccommodationType()
        {
            if (SelectedComboBoxItem == null)
            {
                Type = null;
                return;
            }

            switch (SelectedComboBoxItem.Content.ToString())
            {
                case "Apartman":
                    Type = AccommodationType.Apartment;
                    break;
                case "Kuća":
                    Type = AccommodationType.House;
                    break;
                case "Koliba":
                    Type = AccommodationType.Cabin;
                    break;
                default:
                    Type = null;
                    break;
            }

            // Trigger the search whenever the Type is updated
            SearchAccommodation();
        }
        private bool ValidateSearchParams(SearchParams searchParams)
        {
            if (!ValidateIntegerParameter(searchParams.MaxGuests, "maksimalan broj gostiju") ||
                !ValidateIntegerParameter(searchParams.MinReservationDays, "minimalan broj dana rezervacije"))
            {
                return false;
            }

            return true;
        }

        private bool ValidateIntegerParameter(int? parameter, string parameterName)
        {
            if (parameter < 0)
            {
                MessageBox.Show($"Nevažeći format za {parameterName}.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private List<Accommodation> FilterAccommodationsByCriteria(SearchParams searchParams)
        {
            List<Accommodation> accommodations = GetInitialAccommodations(searchParams);

            accommodations = FilterByName(accommodations, searchParams.Name);
            accommodations = FilterByCity(accommodations, searchParams.City);
            accommodations = FilterByCountry(accommodations, searchParams.Country);
            accommodations = FilterByType(accommodations, searchParams.Type);
            accommodations = FilterByMaxGuests(accommodations, searchParams.MaxGuests);
            accommodations = FilterByMinReservationDays(accommodations, searchParams.MinReservationDays);

            return accommodations;
        }

        private List<Accommodation> GetInitialAccommodations(SearchParams searchParams)
        {
            return searchParams switch
            {
                { Name: var name } when !string.IsNullOrWhiteSpace(name) =>
                    _service.FindByName(name),

                { City: var city } when !string.IsNullOrWhiteSpace(city) =>
                    _service.FindByCity(city),

                { Country: var country } when !string.IsNullOrWhiteSpace(country) =>
                    _service.FindByCountry(country),

                { Type: var type } when type.HasValue =>
                    _service.FindByType(type.Value),

                { MaxGuests: var maxGuests } when maxGuests.HasValue =>
                    _service.FindByMaxGuests(maxGuests.Value),

                { MinReservationDays: var minDays } when minDays.HasValue =>
                    _service.FindByMinReservationDays(minDays.Value),

                _ => _service.GetAll()
            };
        }

        private List<Accommodation> Filter(List<Accommodation> accommodations, Func<Accommodation, bool> predicate)
        {
            return accommodations.Where(predicate).ToList();
        }

        private List<Accommodation> FilterByName(List<Accommodation> accommodations, string name)
        {
            return string.IsNullOrWhiteSpace(name) ? accommodations : Filter(accommodations, a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        private List<Accommodation> FilterByCity(List<Accommodation> accommodations, string city)
        {
            return string.IsNullOrWhiteSpace(city) ? accommodations : Filter(accommodations, a => a.Location.City.Contains(city, StringComparison.OrdinalIgnoreCase));
        }

        private List<Accommodation> FilterByCountry(List<Accommodation> accommodations, string country)
        {
            return string.IsNullOrWhiteSpace(country) ? accommodations : Filter(accommodations, a => a.Location.Country.Contains(country, StringComparison.OrdinalIgnoreCase));
        }

        private List<Accommodation> FilterByType(List<Accommodation> accommodations, AccommodationType? type)
        {
            return !type.HasValue ? accommodations : Filter(accommodations, a => a.Type == type.Value);
        }

        private List<Accommodation> FilterByMaxGuests(List<Accommodation> accommodations, int? maxGuests)
        {
            return !maxGuests.HasValue ? accommodations : Filter(accommodations, a => a.MaxGuests >= maxGuests.Value);
        }

        private List<Accommodation> FilterByMinReservationDays(List<Accommodation> accommodations, int? minReservationDays)
        {
            return !minReservationDays.HasValue ? accommodations : Filter(accommodations, a => a.MinReservationDays <= minReservationDays.Value);
        }

        public void ShowReservationWindow(SearchAccommodationView currentView)
        {
            Accommodation? selectedAccommodation = SelectedAccommodation;
            if (selectedAccommodation != null)
            {
                if (string.IsNullOrEmpty(selectedAccommodation.Name))
                {
                    MessageBox.Show("Izabrani smeštaj ne postoji. Molimo izaberite drugi smeštaj.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    //selectedAccommodation.Reservations = _reservationService.GetReservationsByAccommodationId(selectedAccommodation.Id);
                    ReserveAccommodationView reserveAccommodationView = new ReserveAccommodationView(User, selectedAccommodation);
                    reserveAccommodationView.ShowDialog();
                    currentView.Close();
                }
            }
        }
        public void ResetSearch()
        {
            Name = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            SelectedComboBoxItem = (ComboBoxItem)CmbType.Items[0];
            Type = null;
            MaxGuests = null;
            MinReservationDays = null;
            FilteredAccommodations = new List<Accommodation>(AllAccommodations);
        }
    }
}
