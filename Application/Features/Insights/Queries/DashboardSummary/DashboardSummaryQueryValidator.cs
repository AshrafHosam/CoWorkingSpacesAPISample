using FluentValidation;

namespace Application.Features.Insights.Queries.DashboardSummary
{
    public class DashboardSummaryQueryValidator : AbstractValidator<DashboardSummaryQuery>
    {
        public DashboardSummaryQueryValidator()
        {
            RuleFor(a => a.BrandId).NotNull().NotEmpty().NotEqual(Guid.Empty).WithMessage("Invalid Brand Id");
        }
    }
}
