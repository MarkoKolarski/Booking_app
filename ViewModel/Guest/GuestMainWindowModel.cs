using BookingApp.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class GuestMainWindowModel : ViewModelBase
    {
        private readonly User _user;
        public ICommand FocusCommand { get; private set; }

        public GuestMainWindowModel(User user)
        {
            _user = user;
            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public string Username
        {
            get { return _user.Username; }
        }

        public User User
        {
            get { return _user; }
        }
    }
}