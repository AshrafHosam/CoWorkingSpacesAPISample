using Domain.Enums;
using FluentValidation;

namespace Application.Features.BrandCosts.Commands.AddRecurringExpense
{
    public class AddRecurringExpenseCommandValidator : AbstractValidator<AddRecurringExpenseCommand>
    {
        public AddRecurringExpenseCommandValidator()
        {
            RuleFor(a => a.NumberOfOccurs)
                .NotEmpty()
                .NotNull()
                .GreaterThan(1);

            RuleFor(a => a.RecurringTimeSpanUnit)
                .NotNull()
                .Must(ValidRecurringTimeSpanUnitEnumValue);
        }

        private bool ValidRecurringTimeSpanUnitEnumValue(RecurringTimeSpanUnitEnum value)
        {
            var enumValues = new HashSet<RecurringTimeSpanUnitEnum>((IEnumerable<RecurringTimeSpanUnitEnum>)Enum.GetValues(typeof(RecurringTimeSpanUnitEnum)));

            return enumValues.Contains(value);
        }
    }
}
