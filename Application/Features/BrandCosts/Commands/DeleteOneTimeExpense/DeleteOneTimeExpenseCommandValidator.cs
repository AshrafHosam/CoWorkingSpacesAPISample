using FluentValidation;

namespace Application.Features.BrandCosts.Commands.DeleteOneTimeExpense
{
    public class DeleteOneTimeExpenseCommandValidator : AbstractValidator<DeleteOneTimeExpenseCommand>
    {
        public DeleteOneTimeExpenseCommandValidator()
        {
            RuleFor(a => a.ExpenseId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
        }
    }
}
