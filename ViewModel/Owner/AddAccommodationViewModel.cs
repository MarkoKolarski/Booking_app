using BookingApp.Dto;
using BookingApp.Model;
using BookingApp.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.ViewModel.Owner
{
    public class AddAccommodationViewModel : ViewModelBase
    {
        private string _imageFolderPath = "../../../Resources/Images/";
        private string _relativePath = "../Resources/Images/";
        private readonly AccommodationService _service;
        private User CurrentUser;
        public AccommodationDto Accommodation { get; set; }
        public AddAccommodationViewModel(User currentUser)
        {
            _service = new AccommodationService();
            CurrentUser = currentUser;
        }
        public void AddAccommodation()
        {
            IsTextBoxEmpty();
            Accommodation accommodation = DtoToAccommodation(Accommodation);
            Accommodation savedAccommodation = _service.Save(accommodation);
            if (savedAccommodation != null)
            {
                MessageBox.Show("Accommodation saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("An error occurred while saving the accommodation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private Accommodation DtoToAccommodation(AccommodationDto dto)
        {
            return new Accommodation
            {
                Name = dto.Name,
                Location = dto.Location,
                Type = dto.Type,
                MaxGuests = dto.MaxGuests,
                MinReservationDays = dto.MinReservationDays,
                OwnerId = CurrentUser.Id,
                Pictures = dto.Pictures
            };
        }
        private void IsTextBoxEmpty()
        {
            if (IsTextBoxEmpty(Accommodation.Name, Accommodation.Location.City, Accommodation.Location.Country, Accommodation.MaxGuests.ToString(), Accommodation.MinReservationDays.ToString()))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_service.CheckIfExist(Accommodation.Name, Accommodation.Location, Accommodation.Type))
            {
                MessageBox.Show("Accommodation already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private bool IsTextBoxEmpty(params string[] textBoxes)
        {
            return textBoxes.Any(tb => string.IsNullOrEmpty(tb));
        }
        public void AddImages()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    string destinationPath = Path.Combine(_imageFolderPath, Path.GetFileName(fileName));
                    File.Copy(fileName, destinationPath, overwrite: true);
                    string relativePath = $"{_relativePath}{Path.GetFileName(fileName)}";
                    Accommodation.Pictures.Add(relativePath);
                }
            }
        }
    }
}
