using FluentValidation;

namespace Application.Features.Reservations.Commands.EditReservation
{
    public class EditReservationCommandValidator : AbstractValidator<EditReservationCommand>
    {
        public EditReservationCommandValidator()
        {
            RuleFor(a => a).Must(ValidateCreateReservationCommandPrice).WithMessage("Invalid Reservation type");
            RuleFor(a => a.ReservationId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(a => a.ClientId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(a => a.AreaId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(a => a.StartDate).NotEqual(DateTime.MinValue).WithMessage("Start date must be specified.");
            RuleFor(a => a.EndDate).NotEqual(a => a.StartDate).GreaterThan(a => a.StartDate).WithMessage("End date must be Greater than start Date.");
        }
        private bool ValidateCreateReservationCommandPrice(EditReservationCommand command)
        {
            if (command.IsHourlyReservation is false && command.IsDailyReservation is false && command.IsMonthlyReservation is false)
                return false;

            return true;
        }
    }
}
