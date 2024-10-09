using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Utils
{
    public static class ReservationUtils
    {

        private static readonly RenovationService _renovationService = new RenovationService();

        /// <summary>
        /// Checks if the given date is available for the specified accommodation.
        /// </summary>
        /// <param name="accommodation">The accommodation to check.</param>
        /// <param name="date">The date to check.</param>
        /// <returns>True if the date is available, false otherwise.</returns>
        public static bool IsAvailable(Accommodation accommodation, DateTime date)
        {

            return !IsDateReserved(accommodation, date) && !IsDateUnderRenovation(accommodation, date);
        }

        private static bool IsDateReserved(Accommodation accommodation, DateTime date)
        {

            foreach (var reservation in accommodation.Reservations)
            {
                if (date >= reservation.ReservedDate.Item1 && date <= reservation.ReservedDate.Item2)
                {
                    return true;
                }
            }
            return false;
        }






        private static bool IsDateUnderRenovation(Accommodation accommodation, DateTime date)
        {
            var activeRenovations = _renovationService.GetActiveByAccommodationId(accommodation.Id);
            foreach (var renovation in activeRenovations)
            {
                if (date >= renovation.StartDate && date <= renovation.EndDate)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the given date range is available for the specified accommodation.
        /// </summary>
        /// <param name="accommodation">The accommodation to check.</param>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>True if the entire range is available, false otherwise.</returns>
        public static bool IsRangeAvailable(Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (!IsAvailable(accommodation, date))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets a list of available dates for the specified accommodation and date range.
        /// </summary>
        /// <param name="accommodation">The accommodation to check.</param>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <param name="numberOfDays">The minimum number of consecutive available days required.</param>
        /// <returns>A list of available dates.</returns>
        public static List<DateTime> GetAvailableDates(Accommodation accommodation, DateTime startDate, DateTime endDate, int? numberOfDays)
        {
            List<DateTime> availableDates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                bool isRangeAvailable = CheckDateRange(accommodation, date, endDate, numberOfDays);

                AddDateIfRangeAvailable(availableDates, date, isRangeAvailable);
            }

            return availableDates;
        }

        private static bool CheckDateRange(Accommodation accommodation, DateTime date, DateTime endDate, int? numberOfDays)
        {
            bool isRangeAvailable = true;

            for (int i = 0; i < numberOfDays.Value; i++)
            {
                DateTime currentDate = date.AddDays(i);

                if (currentDate > endDate || !IsAvailable(accommodation, currentDate))
                {
                    isRangeAvailable = false;
                    break;
                }
            }

            return isRangeAvailable;
        }

        private static void AddDateIfRangeAvailable(List<DateTime> availableDates, DateTime date, bool isRangeAvailable)
        {
            if (isRangeAvailable)
            {
                availableDates.Add(date);
            }
        }

        /// <summary>
        /// Finds alternative available dates for the specified accommodation and start date.
        /// </summary>
        /// <param name="accommodation">The accommodation to check.</param>
        /// <param name="startDate">The start date of the requested reservation.</param>
        /// <param name="numberOfDays">The minimum number of consecutive available days required.</param>
        /// <returns>A list of alternative available dates.</returns>
        public static List<DateTime> FindAlternativeDates(Accommodation accommodation, DateTime startDate, int? numberOfDays)
        {
            var (alternativeDates, endDate, currentDate) = InitializeVariablesForAlternativeDates(startDate);

            while (alternativeDates.Count < numberOfDays && currentDate <= endDate)
            {
                currentDate = CheckAndAddAlternativeDate(accommodation, currentDate, endDate, numberOfDays, alternativeDates);
            }

            return alternativeDates;
        }

        private static (List<DateTime>, DateTime, DateTime) InitializeVariablesForAlternativeDates(DateTime startDate)
        {
            var alternativeDates = new List<DateTime>();
            var endDate = startDate.AddDays(59); // Limit for searching alternative dates
            var currentDate = startDate;

            return (alternativeDates, endDate, currentDate);
        }

        private static DateTime CheckAndAddAlternativeDate(Accommodation accommodation, DateTime currentDate, DateTime endDate, int? numberOfDays, List<DateTime> alternativeDates)
        {
            if (!IsAvailable(accommodation, currentDate))
            {
                return currentDate.AddDays(1);
            }

            var rangeStart = currentDate;
            var rangeEnd = currentDate.AddDays((double)(numberOfDays ?? 0) - 1); // Cast int? to double

            if (rangeEnd > endDate || !IsRangeAvailable(accommodation, rangeStart, rangeEnd))
            {
                return currentDate.AddDays(1);
            }

            alternativeDates.Add(rangeStart);
            return rangeEnd.AddDays(1);
        }
    }
}
