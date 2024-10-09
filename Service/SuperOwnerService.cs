using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    internal class SuperOwnerService
    {
        private readonly ISuperOwnerRepository _superOwnerRepository;
        public SuperOwnerService()
        {
            _superOwnerRepository = Injector.CreateInstance<ISuperOwnerRepository>();
        }

        public List<SuperOwner> GetAll()
        {
            return _superOwnerRepository.GetAll();
        }

        public SuperOwner Save(SuperOwner owner)
        {
            return _superOwnerRepository.Save(owner);
        }

        public void Delete(SuperOwner owner)
        {
            _superOwnerRepository.Delete(owner);
        }

        public SuperOwner Update(SuperOwner owner)
        {
            return _superOwnerRepository.Update(owner);
        }

        public SuperOwner GetByOwnerId(int ownerId)
        {
            return _superOwnerRepository.GetByOwnerId(ownerId);
        }

    }
}
