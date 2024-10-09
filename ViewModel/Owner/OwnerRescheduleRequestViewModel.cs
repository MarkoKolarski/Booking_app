using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.ViewModel.Owner
{
    internal class OwnerRescheduleRequestViewModel : ViewModelBase
    {
        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        public ObservableCollection<RescheduleReservationRequestDto> RescheduleReservationRequests { get; set; }
        public RelayCommand ShowSingleRequest { get; set; }
        public RelayCommand GetBack { get; set; }
        private User _currentUser { get; set; }
        public NavigationService NavService { get; set; }

        public OwnerRescheduleRequestViewModel(User user, NavigationService navService)
        {
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            _currentUser = user;
            LoadRescheduleReservationRequests();
            ShowSingleRequest = new RelayCommand(ShowSingleRequestExecute);
            NavService = navService;
            GetBack = new RelayCommand(GetBackAction);
        }

        public void ShowSingleRequestExecute(object parameter)
        {
            RescheduleReservationRequestDto request = (RescheduleReservationRequestDto)parameter;
            SingleRescheduleRequestPage singleRescheduleRequestPage = new SingleRescheduleRequestPage(request.Id, NavService);
            NavService.Navigate(singleRescheduleRequestPage);
        }

        private void LoadRescheduleReservationRequests()
        {
            RescheduleReservationRequests = new ObservableCollection<RescheduleReservationRequestDto>
            (_rescheduleReservationRequestService.GetByOwnerId(_currentUser.Id).Select(r => EntityToDto(r)));
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
        public void GetBackAction(object parameter)
        {
            //NavService.GoBack();
        }
    }
}
