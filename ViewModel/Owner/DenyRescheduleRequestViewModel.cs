using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    public class DenyRescheduleRequestViewModel : ViewModelBase
    {
        public RescheduleReservationRequestDto RescheduleReservationRequest { get; set; }
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        private RescheduleReservationRequest _rescheduleReservationRequest;
        public NavigationService NavService { get; set; }
        public RelayCommand SendResponse { get; set; }
        public RelayCommand GetBack { get; set; }
        public bool alreadyApproved { get; set; }

        public DenyRescheduleRequestViewModel(int rescheduleReservationRequestId, NavigationService navigationService)
        {
            RescheduleReservationRequest = EntityToDto(_rescheduleReservationRequestService.GetById(rescheduleReservationRequestId));
            SendResponse = new RelayCommand(SendResponseExecute);
            NavService = navigationService;
            GetBack = new RelayCommand(GetBackAction);
        }

        public void SendResponseExecute(object parameter)
        {
            _rescheduleReservationRequestService.Update(RescheduleReservationRequestDto.ToEntity(RescheduleReservationRequest));
            NavService.GoBack();
        }
        public void GetBackAction(object parameter)
        {
            NavService.GoBack();
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
