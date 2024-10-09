using BookingApp.Model;
using BookingApp.ViewModel;
using System;

namespace BookingApp.Dto
{
    public class CommentDto : ViewModelBase
    {
        private readonly Comment _comment;

        public string Text => _comment.Text;
        public string Username => _comment.User.Username;
        public string UserRole => _comment.User.Role.ToString() == "Guest" ? "Gost" : "Vlasnik";
        public DateTime CreationTime => _comment.CreationTime;
        public bool IsFromVisitor => _comment.IsFromVisitor;

        public CommentDto(Comment comment)
        {
            _comment = comment;
        }
    }
}
