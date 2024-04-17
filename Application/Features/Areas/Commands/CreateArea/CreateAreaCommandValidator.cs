using Application.Features.Areas.Common;
using FluentValidation;

namespace Application.Features.Areas.Commands.CreateArea
{
    public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
    {
        public CreateAreaCommandValidator()
        {
            RuleFor(a => a).Must(ValidPricingPlanAttributes).WithMessage("Invalid Default Pricing Model");
            RuleFor(a => a.BranchId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(a => a.AreaTypeId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(a => a.SharedAreaPricingDTO).Must(BeValidSharedAreaPricing).WithMessage("Invalid Shared Area Pricing");
        }

        private bool BeValidSharedAreaPricing(SharedAreaPricingDto dto)
        {
            if (dto == null)
                return true;

            return dto.PricePerHour != 0;
        }

        private bool ValidPricingPlanAttributes(CreateAreaCommand command)
        {
            if (command.BookableAreaPricingDTO is null && command.SharedAreaPricingDTO is null)
                return false;

            return true;
        }
    }
}
