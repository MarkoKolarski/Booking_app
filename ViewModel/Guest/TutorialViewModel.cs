using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class TutorialViewModel : ViewModelBase
    {
        public User User { get; set; }
        public ICommand FocusCommand { get; private set; }
        public TutorialViewModel(User user)
        { 
            User = user;
            FocusCommand = new RelayCommand(ExecuteFocusCommand);

        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }
    }
}
