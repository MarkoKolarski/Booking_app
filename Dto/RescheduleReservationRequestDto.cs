using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class RescheduleReservationRequestDto : ViewModelBase
    {
        private int id;
        private int guest;
        private Reservation reservation;
        private RequestStatus status;
        private string? ownerResponse;
        private Tuple<DateTime, DateTime>? newReservedDate;
        private bool isRead;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int Guest
        {
            get => guest;
            set
            {
                guest = value;
                OnPropertyChanged(nameof(Guest));
            }
        }

        public Reservation Reservation
        {
            get => reservation;
            set
            {
                reservation = value;
                OnPropertyChanged(nameof(Reservation));
            }
        }

        public RequestStatus Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string? OwnerResponse
        {
            get => ownerResponse;
            set
            {
                ownerResponse = value;
                OnPropertyChanged(nameof(OwnerResponse));
            }
        }

        public Tuple<DateTime, DateTime>? NewReservedDate
        {
            get => newReservedDate;
            set
            {
                newReservedDate = value;
                OnPropertyChanged(nameof(NewReservedDate));
            }
        }

        public bool IsRead
        {
            get => isRead;
            set
            {
                isRead = value;
                OnPropertyChanged(nameof(IsRead));
            }
        }

        public RescheduleReservationRequestDto()
        {
            OwnerResponse = string.Empty;
        }

        public RescheduleReservationRequestDto(int id, int guest, Reservation reservation, RequestStatus status, string? ownerResponse, Tuple<DateTime, DateTime>? newReservedDate, bool isRead)
        {
            Id = id;
            Guest = guest;
            Reservation = reservation;
            Status = status;
            OwnerResponse = ownerResponse;
            NewReservedDate = newReservedDate;
            IsRead = isRead;
        }
        public static RescheduleReservationRequest ToEntity(RescheduleReservationRequestDto dto)
        {
            return new RescheduleReservationRequest()
            {
                Id = dto.Id,
                Guest = new User() { Id = dto.Guest },
                Reservation = dto.Reservation,
                Status = dto.Status,
                OwnerResponse = dto.OwnerResponse,
                NewReservedDate = dto.NewReservedDate,
                IsRead = dto.IsRead
            };
        }
    }

}
