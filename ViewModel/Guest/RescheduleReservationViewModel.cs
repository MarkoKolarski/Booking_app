using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BookingApp.Command;

namespace BookingApp.ViewModel.Guest
{
    public class RescheduleReservationViewModel : ViewModelBase
    {

        private readonly RescheduleReservationRequestService _rescheduleReservationRequestService;
        private Reservation _selectedReservation;
        private Accommodation _selectedAccommodation;

        public Reservation SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public Accommodation SelectedAccommodation
        {
            get { return _selectedAccommodation; }
            set
            {
                _selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                EndDate = _startDate.AddDays(_numberOfDays);
                OnPropertyChanged(nameof(StartDate));
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private int _currentImageIndex = 0;
        public int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            set
            {
                if (SelectedAccommodation != null && SelectedAccommodation.Pictures.Count > 0)
                {
                    _currentImageIndex = value;
                    if (_currentImageIndex < 0)
                    {
                        _currentImageIndex = SelectedAccommodation.Pictures.Count - 1;
                    }
                    else if (_currentImageIndex >= SelectedAccommodation.Pictures.Count)
                    {
                        _currentImageIndex = 0;
                    }
                    OnPropertyChanged(nameof(CurrentImageIndex));
                    OnPropertyChanged(nameof(CurrentImagePath));
                }
            }
        }

        public string CurrentImagePath
        {
            get
            {
                if (SelectedAccommodation != null && SelectedAccommodation.Pictures.Count > 0)
                {
                    return SelectedAccommodation.Pictures[_currentImageIndex];
                }
                return null;
            }
        }

        private readonly int _numberOfDays;

        public User User { get; set; }
        public ICommand FocusCommand { get; private set; }

        public RescheduleReservationViewModel(User user, Reservation selectedReservation)
        {
            _selectedReservation = selectedReservation;
            User = user;
            _rescheduleReservationRequestService = new RescheduleReservationRequestService();
            SelectedAccommodation = _selectedReservation.Accommodation;
            _startDate = _selectedReservation.ReservedDate.Item1.AddDays(1);
            _numberOfDays = _selectedReservation.ReservedDate.Item2.Subtract(_selectedReservation.ReservedDate.Item1).Days;
            _endDate = _startDate.AddDays(_numberOfDays);

            FocusCommand = new RelayCommand(ExecuteFocusCommand);
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }

        public void PreviousImage()
        {
            CurrentImageIndex--;
        }

        public void NextImage()
        {
            CurrentImageIndex++;
        }

        public bool RescheduleReservation()
        {
            if (!IsInputValid()) return false;

            var requestedNewReservation = CreateRescheduleRequest();

            _rescheduleReservationRequestService.Save(requestedNewReservation);

            MessageBox.Show("Rezervacija je uspešno pomerena.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);


            return true;
        }

        private bool IsInputValid()
        {
            return ValidateInput(
                () => { bool isInvalid = StartDate > EndDate; return isInvalid ? "Krajnji datum ne sme biti pre početnog datuma." : null; },
                //() => { bool isInvalid = EndDate <= SelectedReservation.ReservedDate.Item2; return isInvalid ? "Nova rezervacija mora biti posle stare rezervacije." : null; },
                () => { bool isInvalid = StartDate <= DateTime.Today; return isInvalid ? "Početni datum ne može biti u prošlosti." : null; },
                () =>
                {
                    int numberOfDays = (EndDate - StartDate).Days + 1;
                    bool isInvalid = numberOfDays < SelectedAccommodation.MinReservationDays;
                    return isInvalid ? $"Minimalni broj dana rezervacije je {SelectedAccommodation.MinReservationDays}." : null;
                }
            );
        }

        private bool ValidateInput(params Func<string>[] validationRules)
        {
            foreach (var validationRule in validationRules)
            {
                string errorMessage = validationRule();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ShowErrorMessage(errorMessage);
                    return false;
                }
            }

            return true;
        }

        private RescheduleReservationRequest CreateRescheduleRequest()
        {
            return new RescheduleReservationRequest
            {
                Guest = User,
                Reservation = _selectedReservation,
                Status = RequestStatus.Neodlučeno,
                NewReservedDate = new Tuple<DateTime, DateTime>(StartDate, EndDate),
                IsRead = true
            };
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }

    }

}
