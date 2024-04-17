using FluentValidation;

namespace Application.Features.Insights.Queries.ProfitAndLossSummary
{
    public class ProfitAndLossSummaryQueryValidator : AbstractValidator<ProfitAndLossSummaryQuery>
    {
        public ProfitAndLossSummaryQueryValidator()
        {
            RuleFor(a => a.BrandId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}
