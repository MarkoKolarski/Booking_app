using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.Service
{
    public class UserService
    {
        private readonly IUserRepository _userReposetory;

        public UserService()
        {
            _userReposetory = Injector.CreateInstance<IUserRepository>();
        }

        public User FindById(int id)
        {
            return _userReposetory.FindById(id);
        }


        public User GetByUsername(string username)
        {
            return _userReposetory.GetByUsername(username);
        }
    }
}
