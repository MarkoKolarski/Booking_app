using BookingApp.Model;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class ForumDto : ViewModelBase
    {
        private readonly Forum _forum;

        public int Id
        {
            get => _forum.Id;
        }

        public string Location
        {
            get => _forum.Location;
            set => _forum.Location = value;
        }

        public string Title
        {
            get => _forum.Title;
            set => _forum.Title = value;
        }

        public int CreatorId
        {
            get => _forum.Creator.Id;
            set => _forum.Creator.Id = value;
        }

        public string CreatorUsername
        {
            get => _forum.Creator.Username;
            set => _forum.Creator.Username = value;
        }

        public bool IsClosed
        {
            get => _forum.IsClosed;
            set => _forum.IsClosed = value;
        }

        public bool IsVeryUseful
        {
            get => _forum.isVeryUseful;
            set => _forum.isVeryUseful = value;
        }

        public DateTime CreatedAt
        {
            get => _forum.CreatedAt;
            set => _forum.CreatedAt = value;
        }

        //public ObservableCollection<CommentDto> Comments { get; }

        //public ForumDto(Forum forum)
        //{
        //    _forum = forum;
        //    Comments = new ObservableCollection<CommentDto>(
        //        forum.Comments.Select(c => new CommentDto(c))
        //    );
        //}

    }

}
