using BookingApp.Model;
using BookingApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository;

        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
        }

        public List<Forum> GetAllForums()
        {
            return _forumRepository.GetAll();
        }

        public Forum AddForum(Forum forum)
        {
            return _forumRepository.Save(forum);
        }

        public void RemoveForum(Forum forum)
        {
            _forumRepository.Delete(forum);
        }

        public Forum UpdateForum(Forum forum)
        {
            return _forumRepository.Update(forum);
        }

        public Forum GetById(int id)
        {
            return _forumRepository.GetById(id);
        }
    }
}
