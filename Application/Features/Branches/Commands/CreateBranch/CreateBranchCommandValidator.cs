using FluentValidation;

namespace Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchCommandValidator()
        {
            RuleFor(a => a.BrandId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}
