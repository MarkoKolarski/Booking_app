using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.Guide;
using BookingApp.View.Tourist;
using BookingApp.View.Guest;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; // Make the event nullable

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    DirectingRoles(user);
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

        }

        private void DirectingRoles(User user)
        {
            switch (user.Role)
            {
                case Role.Owner:
                    OwnerMainWindow ownerMainWindow = new OwnerMainWindow(user);
                    ownerMainWindow.ShowDialog();
                    break;
                case Role.Guest:
                    GuestMainWindow guestMainWindow = new GuestMainWindow(user);
                    guestMainWindow.Show();
                    break;
                case Role.Guide:
                    GuideMainWindow guideMainWindow = new GuideMainWindow(user.Id);
                    guideMainWindow.Show();
                    break;
                case Role.Tourist:
                    TourWindow tourWindow = new TourWindow(user);
                    tourWindow.Show();
                    break;
            }
        }

    }
}
