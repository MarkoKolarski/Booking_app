using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    public interface IForumRepository
    {
        List<Forum> GetAll();
        Forum GetById(int id);
        Forum Save(Forum forum);
        Forum Update(Forum forum);
        void Delete(Forum forum);
    }
}
