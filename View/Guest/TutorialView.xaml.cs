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
using System.Windows.Threading;

namespace BookingApp.View.Guest
{

    public partial class TutorialView : Window
    {
        private readonly TutorialViewModel _tutorialViewModel;
        private readonly User user;
        private DispatcherTimer timer;
        public TutorialView(User user)
        {
            InitializeComponent();
            this.user = user;
            _tutorialViewModel = new TutorialViewModel(user);
            this.DataContext = _tutorialViewModel;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            titleBar.HandlePreviewKeyDown(e);
            base.OnPreviewKeyDown(e);
        }

        private void StartVideo_Click(object sender, RoutedEventArgs e)
        {
            // Pokreće video
            TutorialVideo.Play();
            timer.Start();
        }

        private void TutorialVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            VideoSlider.Maximum = TutorialVideo.NaturalDuration.TimeSpan.TotalSeconds;
            MessageBox.Show("Uspešno učitan video.");
        }

        private void TutorialVideo_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show($"Nije uspešno učitavanje videa: {e.ErrorException.Message}");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TutorialVideo.NaturalDuration.HasTimeSpan)
            {
                VideoSlider.Value = TutorialVideo.Position.TotalSeconds;
            }
        }

        private void PauseVideo_Click(object sender, RoutedEventArgs e)
        {
            TutorialVideo.Pause();
        }

        private void StopVideo_Click(object sender, RoutedEventArgs e)
        {

            TutorialVideo.Stop();
        }

        private void VideoSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(TutorialVideo.Position.TotalSeconds - e.NewValue) > 1)
            {
                TutorialVideo.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }
    }
}
