using FluentValidation;

namespace Application.Features.BrandCosts.Commands.AddOneTimeExpense
{
    public class AddExpenseCommandValidator : AbstractValidator<AddOneTimeExpenseCommand>
    {
        public AddExpenseCommandValidator()
        {
            RuleFor(a => a.Amount).NotEmpty().GreaterThan(0);
            RuleFor(a => a.CategoryId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(a => a.ExecutionDate).NotEmpty().NotNull();
        }
    }
}
