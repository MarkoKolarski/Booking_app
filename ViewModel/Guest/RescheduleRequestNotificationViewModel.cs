using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class RescheduleRequestNotificationViewModel : ViewModelBase
    {

        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;

        private ObservableCollection<RescheduleReservationRequest> _rescheduleRequests;
        public ObservableCollection<RescheduleReservationRequest> RescheduleRequests
        {
            get { return _rescheduleRequests; }
            set
            {
                _rescheduleRequests = value;
                OnPropertyChanged(nameof(RescheduleRequests));
                OnPropertyChanged(nameof(UnreadNotifications));
            }
        }

        public int UnreadNotifications
        {
            get { return GetUnreadNotificationsCount(); }
        }

        public User User { get; set; }
        public ICommand FocusCommand { get; private set; }

        public RescheduleRequestNotificationViewModel(User user)
        {
            User = user;
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            LoadRescheduleRequests();

            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        private int GetUnreadNotificationsCount()
        {
            return RescheduleRequests.Count(r => !r.IsRead);
        }

        public void LoadRescheduleRequests()
        {
            RescheduleRequests = new ObservableCollection<RescheduleReservationRequest>(
                _rescheduleReservationRequestService.FindByGuestId(User.Id)
                .Where(r => r.Reservation.Deleted == false)
            );
        }
    }
}
