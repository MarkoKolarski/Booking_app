using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.Repository.Interface;

namespace BookingApp.Service
{
    public class SuperGuestService
    {
        private readonly ISuperGuestRepository _superGuestRepository;

        public SuperGuestService()
        {
            _superGuestRepository = Injector.CreateInstance<ISuperGuestRepository>();
        }

        public List<SuperGuest> GetAll()
        {
            return _superGuestRepository.GetAll();
        }

        public SuperGuest GetByGuestId(int guestId)
        {
            return _superGuestRepository.GetByGuestId(guestId);
        }

        public SuperGuest Save(SuperGuest superGuest)
        {
            return _superGuestRepository.Save(superGuest);
        }

        public void Update(SuperGuest updatedSuperGuest)
        {
            _superGuestRepository.Update(updatedSuperGuest);
        }

        public void Delete(SuperGuest superGuest)
        {
            _superGuestRepository.Delete(superGuest);
        }
    }
}