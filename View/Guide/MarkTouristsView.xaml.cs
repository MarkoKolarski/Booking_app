using System.Windows;
using BookingApp.Dto;
using BookingApp.ViewModel.Guide;

namespace BookingApp.View.Guide
{
    /// <summary>
    /// Interaction logic for MarkTouristsView.xaml
    /// </summary>
    public partial class MarkTouristsView : Window
    {
        MarkTouristsViewModel markTouristsViewModel;

        public MarkTouristsView(TourDto tourDto)
        {
            InitializeComponent();
            markTouristsViewModel = new MarkTouristsViewModel(tourDto);
            DataContext = markTouristsViewModel;
        }
        
        private void btnFinishMarkingTourists_Click(object sender, RoutedEventArgs e)
        {
            markTouristsViewModel.FinishMarkingTourists();
            Close();
        }
    }
}
