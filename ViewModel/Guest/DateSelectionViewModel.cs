using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class DateSelectionViewModel : ViewModelBase
    {

        private int _numberOfDays;

        public ObservableCollection<string> AvailableDates { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public ICommand FocusCommand { get; private set; }

        public DateSelectionViewModel(List<DateTime> availableDates, int numberOfDays)
        {
            _numberOfDays = numberOfDays;

            AvailableDates = new ObservableCollection<string>(availableDates.Select(d => $"{d.ToString("dd/MM/yyyy")} - {d.AddDays(_numberOfDays - 1).ToString("dd/MM/yyyy")}"));

            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public void ReserveSelectedDates()
        {
            if (AvailableDates.Count == 0)
            {
                MessageBox.Show("Molimo izaberite datum.", "Greška pri izboru datuma", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public DateTime GetSelectedStartDate()
        {
            return SelectedDate;
        }

        public DateTime GetSelectedEndDate()
        {
            return SelectedDate.AddDays(_numberOfDays - 1);
        }
    }
}