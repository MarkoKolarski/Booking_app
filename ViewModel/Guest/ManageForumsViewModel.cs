using BookingApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BookingApp.Command;
using BookingApp.Service;
using System.Collections.Generic;
using System.Windows;
using BookingApp.View.Guest;

namespace BookingApp.ViewModel.Guest
{
    public class ManageForumsViewModel : ViewModelBase
    {
        private string _searchLocation;
        private string _currentSuggestion;
        private string _searchTitle;
        private bool? _showOnlyMyForums;
        private Forum _selectedForum;
        private string _selectedAccommodationLocation;
        private ObservableCollection<string> _accommodationLocations;
        private ObservableCollection<string> _suggestions;

        public User User { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        public ObservableCollection<Forum> AllForums { get; set; }
        public ObservableCollection<string> AccommodationLocations
        {
            get => _accommodationLocations;
            set
            {
                _accommodationLocations = value;
                OnPropertyChanged(nameof(AccommodationLocations));
            }
        }

        public ObservableCollection<string> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                OnPropertyChanged(nameof(Suggestions));
            }
        }

        public string CurrentSuggestion
        {
            get => _currentSuggestion;
            set
            {
                _currentSuggestion = value;
                OnPropertyChanged(nameof(CurrentSuggestion));
            }
        }

        public string SelectedAccommodationLocation
        {
            get => _selectedAccommodationLocation;
            set
            {
                _selectedAccommodationLocation = value;
                OnPropertyChanged(nameof(SelectedAccommodationLocation));
                SearchLocation = value;
            }
        }

        public string SearchLocation
        {
            get => _searchLocation;
            set
            {
                _searchLocation = value;
                OnPropertyChanged(nameof(SearchLocation));
                UpdateSuggestions();
                SearchForums();
            }
        }

        public string SearchTitle
        {
            get => _searchTitle;
            set
            {
                _searchTitle = value;
                OnPropertyChanged(nameof(SearchTitle));
                SearchForums();
            }
        }

        public bool? ShowOnlyMyForums
        {
            get => _showOnlyMyForums;
            set
            {
                _showOnlyMyForums = value;
                OnPropertyChanged(nameof(ShowOnlyMyForums));
                SearchForums();
            }
        }

        public Forum SelectedForum
        {
            get => _selectedForum;
            set
            {
                _selectedForum = value;
                OnPropertyChanged(nameof(SelectedForum));
            }
        }

        public ICommand OpenForumCommand { get; }
        public ICommand CloseForumCommand { get; }
        public ICommand ShowForumCommand { get; }
        public ICommand CancelSearchCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand FocusCommand { get; private set; }

        private readonly ForumService _forumService;
        private readonly CommentService _commentService;
        private readonly AccommodationService _accommodationService;
        private readonly UserService _userService;
        private readonly ReservationService _reservationService;

        public ManageForumsViewModel(User user)
        {
            _accommodationService = new AccommodationService();
            _forumService = new ForumService();
            _commentService = new CommentService();
            _userService = new UserService();
            _reservationService = new ReservationService();
            User = user;
            Forums = new ObservableCollection<Forum>();
            AllForums = new ObservableCollection<Forum>();
            Suggestions = new ObservableCollection<string>();

            AccommodationLocations = new ObservableCollection<string>(
                _accommodationService.GetAllLocations()
                .Select(t => $"{t.Item1}, {t.Item2}")
            );

            OpenForumCommand = new RelayCommand(_ => OpenForum());
            CloseForumCommand = new RelayCommand(_ => CloseForum());
            ShowForumCommand = new RelayCommand(_ => ShowForum());
            CancelSearchCommand = new RelayCommand(_ => CancelSearch());
            SearchCommand = new RelayCommand(_ => SearchForums());
            FocusCommand = new RelayCommand(ExecuteFocusCommand);

            CancelSearch();
            LoadForums();
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        private void LoadForums()
        {
            var allForums = _forumService.GetAllForums();

            foreach (var forum in allForums)
            {
                var creator = _userService.FindById(forum.Creator.Id);
                if (creator != null)
                {
                    forum.Creator = creator;
                }
            }

            AllForums = new ObservableCollection<Forum>(allForums.OrderByDescending(f => f.CreatedAt));
            Forums = new ObservableCollection<Forum>(allForums.OrderByDescending(f => f.CreatedAt));
        }

        private void OpenForum()
        {
            if (!ValidateInput())
                return;

            var newForum = CreateNewForum();
            var initialComment = CreateInitialComment(newForum);

            AddForumAndComment(newForum, initialComment);

            AllForums = new ObservableCollection<Forum>(AllForums.OrderByDescending(f => f.CreatedAt));
            SearchForums();
        }

        private void AddForumAndComment(Forum newForum, Comment initialComment)
        {
            newForum.Comments.Add(initialComment);
            AllForums.Add(newForum);
            _forumService.AddForum(newForum);
            initialComment.ForumId = newForum.Id;
            _commentService.AddComment(initialComment);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(SearchLocation) || string.IsNullOrWhiteSpace(SearchTitle))
            {
                ShowError("Morate uneti i lokaciju i pitanje da bi ste otvorili novi forum.");
                return false;
            }

            if (!AccommodationLocations.Any(l => l.Equals(SearchLocation, StringComparison.Ordinal)))
            {
                ShowError("Unesena lokacija ne postoji. Molimo odaberite jednu od sugerisanih lokacija. (pazite na velika slova)");
                return false;
            }

            return true;
        }

        private Forum CreateNewForum()
        {
            var newForum = new Forum
            {
                Location = SearchLocation,
                Title = SearchTitle,
                Comments = new List<Comment>(),
                CreatedAt = DateTime.Now,
                Creator = User,
                IsClosed = false,
                isVeryUseful = false
            };

            return newForum;
        }

        private Comment CreateInitialComment(Forum forum)
        {
            var initialComment = new Comment
            {
                ForumId = forum.Id,
                CreationTime = DateTime.Now,
                Text = SearchTitle,
                User = User,
                IsFromVisitor = false
            };

            if (_reservationService.HasVisitedLocation(User.Id, forum.Location))
            {
                initialComment.IsFromVisitor = true;
            }

            return initialComment;
        }

        private void UpdateSuggestions()
        {
            if (string.IsNullOrWhiteSpace(SearchLocation))
            {
                Suggestions.Clear();
                CurrentSuggestion = string.Empty;
                return;
            }

            var suggestions = AccommodationLocations
                .Where(l => l.StartsWith(SearchLocation, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Suggestions = new ObservableCollection<string>(suggestions);
            CurrentSuggestion = suggestions.FirstOrDefault() ?? string.Empty;
        }

        private bool ShowError(string message, bool returnValue = true)
        {
            MessageBox.Show(message, "Greška pri pretrazi", MessageBoxButton.OK, MessageBoxImage.Error);
            return returnValue;
        }

        private void CloseForum()
        {
            if (SelectedForum != null && SelectedForum.Creator.Id == User.Id)
            {
                SelectedForum.Close();
                _forumService.UpdateForum(SelectedForum);
                SearchForums();
            }
            else
            {
                ShowError("Nemate dozvolu da zatvorite ovaj forum.");
            }
        }

        private void ShowForum()
        {
            if (SelectedForum != null)
            {

                var forumCommentsView = new ForumCommentsView(User, SelectedForum);
                CloseWindow();
                forumCommentsView.Show();
            }
            else
            {
                ShowError("Morate selektovati forum da biste videli komentare.");
            }
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }


        private void CancelSearch()
        {
            SearchLocation = string.Empty;
            SearchTitle = string.Empty;
            ShowOnlyMyForums = false;
            SearchForums();
        }

        private void SearchForums()
        {
            var filteredForums = FilterForums();

            Forums = new ObservableCollection<Forum>(filteredForums);
            OnPropertyChanged(nameof(Forums));
        }

        private IEnumerable<Forum> FilterForums()
        {
            var filteredForums = AllForums.AsEnumerable();

            if (!string.IsNullOrEmpty(SearchLocation))
            {
                filteredForums = filteredForums.Where(f => f.Location.Contains(SearchLocation, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(SearchTitle))
            {
                filteredForums = filteredForums.Where(f => f.Title.Contains(SearchTitle, StringComparison.OrdinalIgnoreCase));
            }

            if (ShowOnlyMyForums.HasValue && ShowOnlyMyForums.Value)
            {
                filteredForums = filteredForums.Where(f => f.Creator.Id == User.Id);
            }

            return filteredForums;
        }
    }
}
