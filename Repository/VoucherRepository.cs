using BookingApp.Model;
using System.Collections.Generic;
using BookingApp.Serializer;
using BookingApp.Repository.Interface;
using System.Linq;
using System;

namespace BookingApp.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }

        public List<Voucher> GetAll()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers;
        }

        public List<Voucher> GetAllByTouristId(int touristId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Where(st => st.TouristId == touristId)
                .OrderBy(st => st.Expiration)
                .ToList();
        }

        public Voucher GetByExpiration(int touristId, DateTime expiration)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Where(v => v.Expiration == expiration && v.TouristId == touristId)
                .FirstOrDefault();
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.Any() ? _vouchers.Max(c => c.Id) + 1 : 1;
        }

        public void DeleteSelected(int touristId, Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.RemoveAll(v => v.TouristId == touristId && v.Id == voucher.Id);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public void DeleteByDate(List<Voucher> vouchers)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            vouchers.RemoveAll(v => v.Expiration.Date <= DateTime.Today);
            _serializer.ToCSV(FilePath, vouchers);
        }
    }
}
