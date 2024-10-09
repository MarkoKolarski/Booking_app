using BookingApp.Model;
using System;
using System.Windows;
using System.Windows.Input;
using BookingApp.Command;
using System.Linq;
using BookingApp.Service;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace BookingApp.ViewModel.Guest
{
    public class ReservationInformationViewModel : ViewModelBase
    {
        public User User { get; set; }
        public Reservation SelectedReservation { get; set; }

        public string GuestUsername => SelectedReservation.Guest?.Username ?? string.Empty;
        public string GuestEmail => SelectedReservation.Guest?.Email ?? string.Empty;
        public string GuestPicture => SelectedReservation.Guest?.Picture ?? string.Empty;
        public string AccommodationName => SelectedReservation.Accommodation?.Name ?? string.Empty;
        public string AccommodationCity => SelectedReservation.Accommodation?.Location.City ?? string.Empty;
        public string AccommodationCountry => SelectedReservation.Accommodation?.Location.Country ?? string.Empty;
        public string AccommodationType
        {
            get
            {
                string accommodationType = SelectedReservation.Accommodation?.Type.ToString() ?? string.Empty;

                switch (accommodationType)
                {
                    case "Apartment":
                        return "Apartman";
                    case "Cabin":
                        return "Koliba";
                    case "House":
                        return "Kuća";
                    default:
                        return accommodationType;
                }
            }
        }
        public string AccommodationFirstPicture => SelectedReservation.Accommodation?.Pictures?.FirstOrDefault() ?? string.Empty;
        public string AccommodationOwnerUsername { get; set; }
        public string AccommodationOwnerEmail { get; set; }
        public string AccommodationOwnerPicture { get; set; }
        public int MaxGuests => SelectedReservation.Accommodation?.MaxGuests ?? 0;
        public int MinReservationDays => SelectedReservation.Accommodation?.MinReservationDays ?? 0;
        public int CancellationDays => SelectedReservation.Accommodation?.CancellationDays ?? 0;
        public DateTime ReservedDateStart => SelectedReservation.ReservedDate?.Item1 ?? DateTime.MinValue;
        public DateTime ReservedDateEnd => SelectedReservation.ReservedDate?.Item2 ?? DateTime.MinValue;
        public int NumberOfGuests => SelectedReservation.NumberOfGuests;
        public DateTime CurrentDate => DateTime.Now;

        public ICommand CreateReportCommand { get; private set; }
        public ICommand FocusCommand { get; private set; }
        private readonly UserService _userService;

        public ReservationInformationViewModel(User user, Reservation selectedReservation)
        {
            User = user;
            SelectedReservation = selectedReservation;
            FocusCommand = new RelayCommand(ExecuteFocusCommand);
            CreateReportCommand = new RelayCommand(GeneratePdfReport);
            _userService = new UserService();
            LoadAccommodationOwnerUsername();
        }

        private void LoadAccommodationOwnerUsername()
        {
            if (SelectedReservation?.Accommodation?.OwnerId != null)
            {
                var owner = _userService.FindById(SelectedReservation.Accommodation.OwnerId);
                AccommodationOwnerUsername = owner?.Username ?? string.Empty;
                AccommodationOwnerEmail = owner?.Email ?? string.Empty;
                AccommodationOwnerPicture = owner?.Picture ?? string.Empty;
            }
        }

        public void GeneratePdfReport(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF Report";
            saveFileDialog.FileName = $"reservation_information.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;

                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                Font titleFont = new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD, BaseColor.BLACK);
                Font subtitleFont = new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLACK);
                Font contentFont = new Font(Font.FontFamily.HELVETICA, 14, Font.NORMAL, BaseColor.BLACK);
                Font labelFont = new Font(Font.FontFamily.HELVETICA, 16, Font.NORMAL, BaseColor.BLACK);

                Chunk companyChunk = new Chunk("Booking App", new Font(Font.FontFamily.HELVETICA, 40, Font.BOLD));
                Paragraph companyTitle = new Paragraph(companyChunk);
                companyTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(companyTitle);

                doc.Add(new Paragraph("\n"));

                Paragraph mainTitle = new Paragraph("Informacije o rezervaciji", titleFont);
                mainTitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(mainTitle);

                doc.Add(new Paragraph("\n"));

                doc.Add(new Paragraph($"Naziv: {AccommodationName}", contentFont));
                doc.Add(new Paragraph($"Lokacija: {AccommodationCity}, {AccommodationCountry}", contentFont));
                doc.Add(new Paragraph($"Tip: {AccommodationType}", contentFont));
                doc.Add(new Paragraph($"Datum: {ReservedDateStart:dd.MM.yyyy} - {ReservedDateEnd:dd.MM.yyyy}", contentFont));
                doc.Add(new Paragraph($"Broj gostiju: {NumberOfGuests}", contentFont));
                doc.Add(new Paragraph($"Vlasnik: {AccommodationOwnerUsername}", contentFont));
                doc.Add(new Paragraph($"Email vlasnika: {AccommodationOwnerEmail}", contentFont));
                doc.Add(new Paragraph($"Gost: {GuestUsername}", contentFont));
                doc.Add(new Paragraph($"Email gosta: {GuestEmail}", contentFont));

                doc.Add(new Paragraph("\n"));

                // Dodajemo posebno obojene informacije
                Paragraph currentDateParagraph = new Paragraph($"Današnji datum: {CurrentDate:dd.MM.yyyy}", contentFont);
                currentDateParagraph.Font.Color = BaseColor.RED;
                doc.Add(currentDateParagraph);

                Paragraph createdByParagraph = new Paragraph($"Kreirao: {GuestUsername}", contentFont);
                createdByParagraph.Font.Color = BaseColor.RED;
                doc.Add(createdByParagraph);

                doc.Close();

                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            }
        }

        private void ExecuteFocusCommand(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Focus();
            }
        }
    }
}
