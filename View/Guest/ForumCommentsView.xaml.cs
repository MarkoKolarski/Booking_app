using BookingApp.Model;
using BookingApp.ViewModel.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.Guest
{

    public partial class ForumCommentsView : Window
    {
        private readonly ForumCommentsViewModel  _viewModel;
        private readonly User user;

        public ForumCommentsView(User user ,Forum selectedForum)
        {
            this.user = user;
            InitializeComponent();
            _viewModel = new ForumCommentsViewModel(user, selectedForum);
            DataContext = _viewModel;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }
    }
}
