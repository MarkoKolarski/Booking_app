using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BookingApp.Command;
using System.Windows;
using BookingApp.Dto;

namespace BookingApp.ViewModel.Guest
{
    public class ForumCommentsViewModel : ViewModelBase
    {
        public User User { get; set; }

        private Forum _selectedForum;
        public Forum SelectedForum
        {
            get { return _selectedForum; }
            set
            {
                _selectedForum = value;
                OnPropertyChanged(nameof(SelectedForum));
            }
        }

        private string _newCommentText;
        public string NewCommentText
        {
            get { return _newCommentText; }
            set
            {
                _newCommentText = value;
                OnPropertyChanged(nameof(NewCommentText));
            }
        }

        private ObservableCollection<CommentDto> _comments;
        public ObservableCollection<CommentDto> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public ICommand AddCommentCommand { get; }
        public ICommand FocusCommand { get; private set; }

        private readonly ForumService _forumService;
        private readonly CommentService _commentService;
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;

        public ForumCommentsViewModel(User user, Forum selectedForum)
        {
            _forumService = new ForumService();
            _commentService = new CommentService();
            _accommodationService = new AccommodationService();
            _reservationService = new ReservationService();
            _userService = new UserService();

            User = user;
            InitializeComments(selectedForum);

            AddCommentCommand = new RelayCommand(_ => AddComment());
            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        private void InitializeComments(Forum selectedForum)
        {
            SelectedForum = selectedForum;
            SelectedForum.Comments = _commentService.GetAllCommentsByForumId(SelectedForum.Id);

            foreach (var comment in selectedForum.Comments)
            {
                var userOfComment = _userService.FindById(comment.User.Id);
                if (userOfComment != null)
                {
                    comment.User = userOfComment;
                }
            }
            var commentDtos = SelectedForum.Comments.Select(comment => new CommentDto(comment)).ToList();
            Comments = new ObservableCollection<CommentDto>(commentDtos.OrderBy(c => c.CreationTime));
        }

        private void AddComment()
        {
            if (!CanAddComment())
                return;

            var newComment = CreateNewComment();

            if (!_reservationService.HasVisitedLocation(User.Id, SelectedForum.Location))
                newComment.IsFromVisitor = false;

            AddCommentToForum(newComment);
            UpdateCommentList(newComment);
            ClearNewCommentText();

            _forumService.UpdateForum(SelectedForum);
            CheckAndMarkVeryUseful();
        }

        private bool CanAddComment()
        {
            if (SelectedForum.IsClosed)
            {
                ShowError("Forum je zatvoren, ne možete uneti nove komentare.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewCommentText))
                return false;

            return true;
        }

        private Comment CreateNewComment()
        {
            return new Comment
            {
                ForumId = SelectedForum.Id,
                CreationTime = DateTime.Now,
                Text = NewCommentText,
                User = User,
                IsFromVisitor = true
            };
        }

        private void AddCommentToForum(Comment newComment)
        {
            _commentService.AddComment(newComment);
            SelectedForum.Comments.Add(newComment);
        }

        private void UpdateCommentList(Comment newComment)
        {
            var newCommentDto = new CommentDto(newComment);
            Comments.Add(newCommentDto);
            OnPropertyChanged(nameof(Comments));
        }

        private void ClearNewCommentText()
        {
            NewCommentText = string.Empty;
            OnPropertyChanged(nameof(NewCommentText));
        }

        private bool ShowError(string message, bool returnValue = true)
        {
            MessageBox.Show(message, "Greška pri unosu novog komentara", MessageBoxButton.OK, MessageBoxImage.Error);
            return returnValue;
        }

        private void CheckAndMarkVeryUseful()
        {
            var visitorComments = SelectedForum.Comments.Count(c => c.IsFromVisitor);
            var ownerComments = SelectedForum.Comments.Count(c => _accommodationService.OwnsAccommodationAtLocation(c.User.Id, SelectedForum.Location));

            if (visitorComments >= 20 && ownerComments >= 10)
            {
                SelectedForum.isVeryUseful = true;
                _forumService.UpdateForum(SelectedForum);
                OnPropertyChanged(nameof(SelectedForum));
            }
        }
    }
}
