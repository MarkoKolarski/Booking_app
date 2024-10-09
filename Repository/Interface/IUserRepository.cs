using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    public interface IUserRepository
    {
        User FindById(int id);
        User GetByUsername(string username);
    }
}
