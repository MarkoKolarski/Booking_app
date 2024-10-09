using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.Repository.Interface
{
    public interface ISuperGuestRepository
    {
        List<SuperGuest> GetAll();
        SuperGuest GetByGuestId(int guestId);
        SuperGuest Save(SuperGuest superGuest);
        int NextId();
        void Update(SuperGuest updatedSuperGuest);
        void Delete(SuperGuest superGuest);
    }
}