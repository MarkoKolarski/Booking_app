using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService()
        {
            _commentRepository = Injector.CreateInstance<ICommentRepository>();
        }
    
        public List<Comment> GetAllCommentsByForumId(int forumId)
        {
            return _commentRepository.GetAllByForumId(forumId);
        }

        public List<Comment> GetAllComments()
        {
            return _commentRepository.GetAll();
        }

        public Comment AddComment(Comment comment)
        {
            return _commentRepository.Save(comment);
        }

        public void RemoveComment(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public Comment UpdateComment(Comment comment)
        {
            return _commentRepository.Update(comment);
        }

        public List<Comment> GetCommentsByUser(User user)
        {
            return _commentRepository.GetByUser(user);
        }
    }
}
