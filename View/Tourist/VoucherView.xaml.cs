using BookingApp.Model;
using System.Collections.Generic;
using System.Windows;
using BookingApp.Service;
using BookingApp.Dto;
using BookingApp.ViewModel.Tourist;

namespace BookingApp.View.Tourist
{
    public partial class VoucherView : Window
    {
        private VoucherViewModel voucherViewModel;
        private readonly VoucherService _voucherService;
        private readonly User user;
        private readonly List<Model.Tourist> _participants;
        private readonly EnterPeopleViewModel _enterPeopleViewModel;

        public VoucherView(User user)
        {
            InitializeComponent();
            this.user = user;
            voucherViewModel = new VoucherViewModel(user);
            _voucherService = new VoucherService();
            DataContext = voucherViewModel;
        }

        public VoucherView(User user, List<Model.Tourist> participants, EnterPeopleViewModel enterPeopleViewModel)
        {
            InitializeComponent();
            this.user = user;
            _participants = participants;
            _enterPeopleViewModel = enterPeopleViewModel;
            voucherViewModel = new VoucherViewModel(user, participants);
            _voucherService = new VoucherService();
            DataContext = voucherViewModel;
            btnVoucherReservation.Visibility = Visibility.Visible;
        }

        public void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (voucherViewModel.SelectedVoucher == null)
            {
                MessageBox.Show("Please select a voucher.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _enterPeopleViewModel.Voucher = voucherViewModel.SelectedVoucher;
            _enterPeopleViewModel.MakeReservation();
                                  
            VoucherDto selectedDto = voucherViewModel.SelectedVoucher;
            int id = user.Id;
            Voucher selectedVoucher = voucherViewModel.FindVoucherFromDto(user.Id, selectedDto.Expiration);

            if (selectedVoucher != null)
            {
                _voucherService.DeleteSelected(user.Id, selectedVoucher);
            }

            Close();
        }
    }
}
