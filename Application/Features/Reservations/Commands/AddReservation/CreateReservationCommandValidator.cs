using FluentValidation;

namespace Application.Features.Reservations.Commands.AddReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(a => a).Must(ValidateCreateReservationCommandPrice).WithMessage("Invalid Reservation Type.");

            RuleFor(a => a.ClientId).NotNull().NotEmpty().NotEqual(Guid.Empty);

            RuleFor(a => a.AreaId).NotNull().NotEmpty().NotEqual(Guid.Empty);

            RuleFor(a => a.StartDate).NotNull().NotEqual(DateTime.MinValue).WithMessage("Start date must be specified.");

            RuleFor(a => a.EndDate).NotNull().NotEqual(a => a.StartDate).GreaterThan(a => a.StartDate).WithMessage("End date must be Greater than start Date.");
        }
        private bool ValidateCreateReservationCommandPrice(CreateReservationCommand command)
        {
            if (command.IsHourlyReservation is false && command.IsDailyReservation is false && command.IsMonthlyReservation is false)
                return false;

            return true;
        }
    }
}
