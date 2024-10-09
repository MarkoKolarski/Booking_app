using BookingApp.Command;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BookingApp.ViewModel.Owner
{
    public class OwnerMainWindowViewModel : ViewModelBase
    {
        private DispatcherTimer? _reminderTimer;
        private readonly ReservationRepository _reservationRepository;
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        public RelayCommand AddAccommodation { get; set; }
        public RelayCommand ShowReviews { get; set; }
        public RelayCommand ShowLastCheckouts { get; set; }
        public RelayCommand ShowRescheduleRequests { get; set; }
        public RelayCommand ShowGuestRatings { get; set; }
        public RelayCommand ShowProfilePage { get; set; }
        public RelayCommand ShowAllAccommodations { get; set; }
        public RelayCommand ShowStatistic { get; set; }
        public RelayCommand ShowRenovations { get; set; }
        public User _currentUser { get; set; }
        private Frame _mainFrame { get; set; }

        private string _numberOfNotifications;
        public string NumberOfNotifications
        {
            get => _numberOfNotifications;
            set
            {
                _numberOfNotifications = value;
                OnPropertyChanged(nameof(NumberOfNotifications));
            }
        }
        public OwnerMainWindowViewModel(User user, Frame mainFrame)
        {
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            _currentUser = user;
            _mainFrame = mainFrame;
            AddAccommodation = new RelayCommand(AddAccommodationExecute);
            _reservationRepository = new ReservationRepository();
            ShowNotifications(this, new RoutedEventArgs());
            ShowReviews = new RelayCommand(ShowReviewsExecute);
            ShowLastCheckouts = new RelayCommand(ShowLastCheckoutsExecute);
            ShowRescheduleRequests = new RelayCommand(ShowRescheduleRequestsExecute);
            ShowGuestRatings = new RelayCommand(ShowAllAccommodationsExecute);
            GetNumberOfNotifications();
            ShowProfilePage = new RelayCommand(ShowAllAccommodationsExecute);
            ShowAllAccommodations = new RelayCommand(ShowAllAccommodationsExecute);
            ShowRenovations = new RelayCommand(ShowRenovationsExecute);
            ShowStatistic = new RelayCommand(ShowStatisticExecute);
        }

        public void ShowRenovationsExecute(object parameter)
        {
            ShowAllRenovationView showAllRenovationView = new ShowAllRenovationView();
            _mainFrame.NavigationService.Navigate(showAllRenovationView);
        }
        public void GetNumberOfNotifications()
        {
            NumberOfNotifications = _rescheduleReservationRequestService.GetByOwnerId(_currentUser.Id).Count.ToString();
        }
        public void AddAccommodationExecute(object parameter)
        {
            AddAccommodationView addAccommodationView = new AddAccommodationView(_currentUser);
            _mainFrame.NavigationService.Navigate(addAccommodationView);
        }
        private void ShowReviewsExecute(object parametar)
        {
            OwnerReviews ownerReviews = new OwnerReviews(_currentUser, _mainFrame.NavigationService);
            _mainFrame.NavigationService.Navigate(ownerReviews);
        }
        private void ShowLastCheckoutsExecute(object parametar)
        {
            LastCheckoutsView lastCheckoutsView = new LastCheckoutsView(_currentUser);
            _mainFrame.NavigationService.Navigate(lastCheckoutsView);
        }
        private void ShowRescheduleRequestsExecute(object parametar)
        {
            OwnerRescheduleRequestView ownerRescheduleRequestView = new OwnerRescheduleRequestView(_currentUser, _mainFrame.NavigationService);
            _mainFrame.NavigationService.Navigate(ownerRescheduleRequestView);
        }
        private void ShowGuestRatingsExecute(object parametar)
        {
            RatingsOverview ratingsOverview = new RatingsOverview(_currentUser);
            _mainFrame.NavigationService.Navigate(ratingsOverview);
        }
        private void ShowProfilePageExecute(object parametar)
        {
            OwnerProfilePage ownerProfilePage = new OwnerProfilePage(_currentUser);
            _mainFrame.NavigationService.Navigate(ownerProfilePage);
        }

        public void ShowNotifications(object sender, RoutedEventArgs e)
        {
            if (_reservationRepository.CheckForUnratedReservation())
            {
                StartReminderTimer();
            }
        }

        public void ShowAllAccommodationsExecute(object parameter)
        {
            ShowAllAccommodationsVIew showAllAccommodationsVIew = new ShowAllAccommodationsVIew(_currentUser.Id ,_mainFrame.NavigationService);
            _mainFrame.NavigationService.Navigate(showAllAccommodationsVIew);
        }


        public void ShowStatisticExecute(object parameter)
        {
            AccommodationStatisticView accommodationStatistic = new AccommodationStatisticView(_mainFrame.NavigationService);
            _mainFrame.NavigationService.Navigate(accommodationStatistic);
        }
        private void StartReminderTimer()
        {
            _reminderTimer = new DispatcherTimer();
            _reminderTimer.Interval = TimeSpan.FromSeconds(50);
            _reminderTimer.Tick += ReminderTimerMessage;
            _reminderTimer.Start();
        }

        private void ReminderTimerMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("Please submit your rating for the reservation.");
        }
    }
}
