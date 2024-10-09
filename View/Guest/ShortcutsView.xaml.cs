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
    /// <summary>
    /// Interaction logic for ShortcutsView.xaml
    /// </summary>
    public partial class ShortcutsView : Window
    {
        private readonly ShortcutsViewModel _shortcutsViewModel;
        private readonly User user;
        public ShortcutsView(User user)
        {
            InitializeComponent();
            this.user = user;
            _shortcutsViewModel = new ShortcutsViewModel(user);
            this.DataContext = _shortcutsViewModel;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }
    }
}
