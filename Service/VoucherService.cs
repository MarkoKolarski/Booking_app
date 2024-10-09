using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;

namespace BookingApp.Service
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService()
        {
            _voucherRepository = Injector.CreateInstance<IVoucherRepository>();
        }

        public List<Voucher> GetAll()
        {
            return _voucherRepository.GetAll();
        }

        public List<Voucher> GetAllByTouristId(int touristId)
        {
            return _voucherRepository.GetAllByTouristId(touristId);
        }

        public Voucher GetByExpiration(int touristId, DateTime expiration)
        {
            return _voucherRepository.GetByExpiration(touristId, expiration);
        }

        public Voucher Save(Voucher voucher)
        {
            return _voucherRepository.Save(voucher);
        }

        public void DeleteSelected(int touristId, Voucher voucher)
        {
            _voucherRepository.DeleteSelected(touristId, voucher);
        }

        public void DeleteByDate(List<Voucher> vouchers)
        {
            _voucherRepository.DeleteByDate(vouchers);
        }
    }
}
