using BookingApp.Model;
using BookingApp.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.Tourist
{
    public partial class NotificationView : Window
    {
        private NewTourNotificationViewModel notificationViewModel;
        private readonly User user;

        public NotificationView(User user)
        {
            InitializeComponent();
            this.user = user;
            notificationViewModel = new NewTourNotificationViewModel(user);
            DataContext = notificationViewModel;
        }
    }

}
