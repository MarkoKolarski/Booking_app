using BookingApp.Model;
using BookingApp.Service;
using BookingApp.ViewModel.Guest;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.View.Guest
{
    public partial class SearchAccommodationView : Window
    {
        private readonly AccommodationService _service;
        private readonly SearchAccommodationViewModel _searchAccommodationViewModel;
        public List<Accommodation> AllAccommodations { get; set; }
        public List<Accommodation> FilteredAccommodations { get; set; }

        private readonly User user;

        public SearchAccommodationView(User user)
        {
            InitializeComponent();
            this.user = user;
            _service = new AccommodationService();
            _searchAccommodationViewModel = new SearchAccommodationViewModel(user, cmbType);
            AllAccommodations = _service.GetAll();
            FilteredAccommodations = new List<Accommodation>();
            this.DataContext = _searchAccommodationViewModel;



            GuestMainWindowModel guestMainWindowModel = new GuestMainWindowModel(user);
        }


        private void cmbType_Loaded(object sender, RoutedEventArgs e)
        {
            cmbType.SelectedIndex = 0;
        }

        private void SearchAccommodation(object sender, RoutedEventArgs e)
        {
            _searchAccommodationViewModel.SearchAccommodation();
        }


        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }


        private void CancelSearch_Click(object sender, RoutedEventArgs e)
        {
            _searchAccommodationViewModel.ResetSearch();

        }

        private void ReserveAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Accommodation? selectedAccommodation = lvResults.SelectedItem as Accommodation;
            if (selectedAccommodation != null)
            {
                titleBar.OpenReserveAccommodationView(sender, e, selectedAccommodation);

            }
            else
            {
                MessageBox.Show("Molimo izaberite smeštaj iz liste.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
