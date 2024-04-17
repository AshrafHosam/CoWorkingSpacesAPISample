using FluentValidation;

namespace Application.Features.BrandCosts.Commands.EditOneTimeExpense
{
    public class EditOneTimeExpenseCommandValidator : AbstractValidator<EditOneTimeExpenseCommand>
    {
        public EditOneTimeExpenseCommandValidator()
        {
            RuleFor(a => a.ExpenseId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);

            RuleFor(a => a.CategoryId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);

            RuleFor(a => a.Amount)
                .GreaterThan(0);

            RuleFor(a => a.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
