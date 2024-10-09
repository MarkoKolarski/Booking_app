using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.Interface
{
    public interface ICommentRepository
    {
        List<Comment> GetAllByForumId(int forumId);
        List<Comment> GetAll();
        Comment Save(Comment comment);
        void Delete(Comment comment);
        Comment Update(Comment comment);
        List<Comment> GetByUser(User user);
    }
}
