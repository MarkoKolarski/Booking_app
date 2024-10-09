using BookingApp.Model;
using System;
using System.Collections.Generic;

namespace BookingApp.Repository.Interface
{
    public interface IVoucherRepository
    {
        public List<Voucher> GetAll();
        public List<Voucher> GetAllByTouristId(int touristId);
        public Voucher GetByExpiration(int touristId, DateTime expiration);
        Voucher Save(Voucher voucher);
        public void DeleteSelected(int touristId, Voucher voucher);
        public void DeleteByDate(List<Voucher> vouchers);
    }
}
