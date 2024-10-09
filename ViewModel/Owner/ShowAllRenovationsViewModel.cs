using BookingApp.Command;
using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Owner
{
    internal class ShowAllRenovationsViewModel : ViewModelBase
    {
        private readonly RenovationService _renovationService;
        private ObservableCollection<RenovationDto>? _renovations;
        public ObservableCollection<RenovationDto>? Renovations
        {
            get { return _renovations; }
            set
            {
                _renovations = value;
                OnPropertyChanged(nameof(Renovations));
            }
        }
        public RelayCommand RejectRenovation { get; set; }
        public ShowAllRenovationsViewModel()
        {
            _renovationService = new RenovationService();
            LoadRenovations();
            RejectRenovation = new RelayCommand(RejectRenovationExecute);
        }

        public void RejectRenovationExecute(object parametar)
        {
            RenovationDto renovationDto = (RenovationDto)parametar;
            Renovation renovation = _renovationService.GetById(renovationDto.Id);
            renovation.Status = RenovationStatus.Rejected;
            Renovation updateRenovation = _renovationService.Update(renovation);
            if (updateRenovation == null)
            {
                MessageBox.Show("Renovation is not rejected!");
                return;
            }
            MessageBox.Show("Renovation is rejected!");
            LoadRenovations();
        }
        private void LoadRenovations()
        {
            Renovations = new ObservableCollection<RenovationDto>(_renovationService.GetAll().Select(x => RenovationToDto(x)));
        }
        private RenovationDto RenovationToDto(Renovation renovation)
        {
            return new RenovationDto
            {
                Id = renovation.Id,
                StartDate = renovation.StartDate,
                EndDate = renovation.EndDate,
                AccommodationName = renovation.Accommodation.Name,
                Description = renovation.Description,
                Location = renovation.Accommodation.Location,
                Status = renovation.Status.ToString(),
                ButtonContent = GetButtonVisibility(renovation.StartDate, renovation.Status),
            };
        }
        public string GetButtonVisibility(DateTime startDate, RenovationStatus status)
        {
            if (DateTime.Now.AddDays(5) < startDate && status != RenovationStatus.Rejected)
            {
                return "Visible";
            }
            return "Collapsed";
        }
    }
}
