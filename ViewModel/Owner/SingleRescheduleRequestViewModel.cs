using BookingApp.Command;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.Utils;
using System;
using System.Windows;
using System.Windows.Navigation;
using BookingApp.Dto;

namespace BookingApp.ViewModel.Owner
{
    class SingleRescheduleRequestViewModel : ViewModelBase
    {
        public string IsAvailable
        {
            get => _isAvailable ? "date are available !!!" : "date  is not available !!!";
        }
        public string IsAvailableImage
        {
            get => _isAvailable ? "../Resources/Images/greenCorrect.png" : "../Resources/Images/notCorrect.png";
        }
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        private readonly ReservationService _reservationService;
        private RescheduleReservationRequestDto _rescheduleReservationRequest;
        private Reservation _reservation;
        private bool _isAvailable;
        private bool alreadyApproved = false;
        public RelayCommand ApproveRequest { get; set; }
        public RelayCommand GetBack { get; set; }
        public RelayCommand DenyRequest { get; set; }
        public NavigationService NavService;
        public SingleRescheduleRequestViewModel(int rescheduleReservationRequestId, NavigationService navService)
        {
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            _rescheduleReservationRequest = EntityToDto(_rescheduleReservationRequestService.GetById(rescheduleReservationRequestId));
            _reservation = _rescheduleReservationRequest.Reservation;
            _reservationService = new ReservationService();
            NavService = navService;
            ApproveRequest = new RelayCommand(ApproveRequestExecute);
            DenyRequest = new RelayCommand(DenyRequestExecute);
            GetBack = new RelayCommand(GetBackAction);
            CheckDateAvailability();
        }
        public void ApproveRequestExecute(object parametar)
        {
            CheckIsApproved();
            _rescheduleReservationRequest.Status = RequestStatus.Odobreno;
            _rescheduleReservationRequest.IsRead = false;
            _rescheduleReservationRequestService.Update(RescheduleReservationRequestDto.ToEntity(_rescheduleReservationRequest));
            _reservation.ReservedDate = _rescheduleReservationRequest.NewReservedDate;
            _reservationService.Update(_reservation);
            alreadyApproved = true;
            NavService.GoBack();
        }
        public void CheckIsApproved()
        {
            if (alreadyApproved)
            {
                MessageBox.Show("Request is already approved", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DenyRequestExecute(object parametar)
        {
            CheckIsApproved();
            DenyRescheduleRequest denyRescheduleRequest = new DenyRescheduleRequest(_rescheduleReservationRequest.Id, NavService);

            if (!alreadyApproved)
            {
                NavService.Navigate(denyRescheduleRequest);
            }
            alreadyApproved = true;
        }
        public void CheckDateAvailability()
        {
            _isAvailable = ReservationUtils.IsRangeAvailable(_reservation.Accommodation, _rescheduleReservationRequest.NewReservedDate.Item1, _rescheduleReservationRequest.NewReservedDate.Item2);
            OnPropertyChanged(nameof(IsAvailable));
        }

        private void GetBackAction(object parameter)
        {
            NavService.GoBack();
        }

        public void OnNavigatedTo()
        {
            alreadyApproved = false;
        }
        private RescheduleReservationRequestDto EntityToDto(RescheduleReservationRequest entity)
        {
            return new RescheduleReservationRequestDto
            {
                Id = entity.Id,
                Guest = entity.Guest.Id,
                Reservation = entity.Reservation,
                Status = entity.Status,
                OwnerResponse = entity.OwnerResponse,
                NewReservedDate = entity.NewReservedDate,
                IsRead = entity.IsRead
            };
        }
    }
}