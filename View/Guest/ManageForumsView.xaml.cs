using BookingApp.Model;
using BookingApp.ViewModel.Guest;
using System;
using System.Collections.Generic;
using System.Data;
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

    public partial class ManageForumsView : Window
    {
        private readonly ManageForumsViewModel _viewModel;
        private readonly User user;
        public ManageForumsView(User user)
        {
            this.user = user;
            InitializeComponent();
            _viewModel = new ManageForumsViewModel(user);
            DataContext = _viewModel;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

    }
}
