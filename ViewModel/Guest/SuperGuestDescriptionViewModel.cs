using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class SuperGuestDescriptionViewModel : ViewModelBase
    {
        private readonly SuperGuestService _superGuestService;
        public User User { get; set; }
        private SuperGuestDto? _superGuest;
        public SuperGuestDto? SuperGuest
        {
            get { return _superGuest; }
            set
            {
                if (_superGuest != value)
                {
                    _superGuest = value;
                    OnPropertyChanged(nameof(SuperGuest));
                }
            }
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand FocusCommand { get; private set; }

        public SuperGuestDescriptionViewModel(User user)
        {
            User = user;
            _superGuestService = new SuperGuestService();
            SuperGuest = GetSuperGuestDtoById(User.Id);

            CloseWindowCommand = new RelayCommand(CloseWindow);
            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public SuperGuestDto? GetSuperGuestDtoById(int id)
        {
            var superGuest = _superGuestService.GetByGuestId(id);
            return superGuest != null ? new SuperGuestDto(superGuest) : null;
        }

        private void CloseWindow(object obj)
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
    }
}