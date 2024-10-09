using System;

namespace BookingApp.Dto
{
    public class VoucherDto
    {
        public string Name { get; set; }
        public DateTime Expiration { get; set; }

        public VoucherDto() { }

        public VoucherDto(string name, DateTime expiration)
        {
            Name = name;
            Expiration = expiration;
        }
    }
}
