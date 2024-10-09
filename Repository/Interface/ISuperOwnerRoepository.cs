using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    public interface ISuperOwnerRepository
    {
        List<SuperOwner> GetAll();
        SuperOwner Save(SuperOwner owner);
        void Delete(SuperOwner owner);
        SuperOwner Update(SuperOwner owner);
        SuperOwner GetByOwnerId(int ownerId);
    }
}
