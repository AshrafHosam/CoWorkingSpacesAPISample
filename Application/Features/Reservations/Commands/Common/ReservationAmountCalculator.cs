using Domain.Entities;

namespace Application.Features.Reservations.Commands.Common
{
    internal class ReservationAmountCalculator
    {
        public static decimal CalculateReservationTotalAmount(Reservation reservation, Area area)
        {
            TimeSpan duration = reservation.EndDate - reservation.StartDate;

            if (reservation.IsDailyReservation)
                return Math.Ceiling(duration.Days * area.BookableAreaPricingPlanModel.PricePerDay.Value);

            if (reservation.IsHourlyReservation)
                return Math.Ceiling((decimal)duration.TotalHours * area.BookableAreaPricingPlanModel.PricePerHour.Value);

            if (reservation.IsMonthlyReservation)
            {
                int monthsDifference = (reservation.EndDate.Year - reservation.StartDate.Year) * 12 + reservation.EndDate.Month - reservation.StartDate.Month;

                return Math.Ceiling(monthsDifference * area.BookableAreaPricingPlanModel.PricePerMonth.Value);
            }

            return 0;
        }
    }
}
