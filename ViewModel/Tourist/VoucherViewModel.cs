using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookingApp.ViewModel.Tourist
{
    public class VoucherViewModel : ViewModelBase
    {
        private readonly VoucherService _voucherService;
        private readonly User user;
        private readonly List<Model.Tourist> _participants;

        private ObservableCollection<VoucherDto> _voucherDtos;

        public ObservableCollection<VoucherDto> VoucherDtos
        {
            get { return _voucherDtos; }
            set
            {
                if (_voucherDtos != value)
                {
                    _voucherDtos = value;
                    OnPropertyChanged(nameof(VoucherDtos));
                }
            }
        }

        private VoucherDto _selectedVoucher;

        public VoucherDto SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                OnPropertyChanged(nameof(SelectedVoucher));
            }
        }

        public VoucherViewModel(User user)
        {
            this.user = user;
            _voucherService = new VoucherService();
            VoucherDtos = new ObservableCollection<VoucherDto>();
            ShowAllVouchers();
        }

        public VoucherViewModel(User user, List<Model.Tourist> participants)
        {
            this.user = user;
            _voucherService = new VoucherService();
            VoucherDtos = new ObservableCollection<VoucherDto>();
            _participants = participants;
            ShowAllVouchers();
        }

        public void ShowAllVouchers()
        {
            List<Voucher> allVouchers = _voucherService.GetAll();
            _voucherService.DeleteByDate(allVouchers);
            List<Voucher> allVouchersUser = _voucherService.GetAllByTouristId(user.Id);

            foreach (Voucher voucher in allVouchersUser)
            {
                VoucherDto voucherDto = new VoucherDto("Voucher", voucher.Expiration);
                VoucherDtos.Add(voucherDto);
            }

        }

        public Voucher FindVoucherFromDto(int touristId, DateTime expiration)
        {
            return _voucherService.GetByExpiration(touristId, expiration);
        }

    }
}
