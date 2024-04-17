using FluentValidation;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryOneTimeExpenses
{
    public class GetBrandCostCategoryExpensesQueryValidator : AbstractValidator<GetBrandCostCategoryOneTimeExpensesQuery>
    {
        public GetBrandCostCategoryExpensesQueryValidator()
        {
            RuleFor(a => a).Must(ValidateQueryDatesFilter);
        }
        private bool ValidateQueryDatesFilter(GetBrandCostCategoryOneTimeExpensesQuery query)
        {

            if ((query.FromDate.HasValue && query.ToDate.HasValue) || (!query.FromDate.HasValue && !query.ToDate.HasValue))
                return true;

            return false;
        }

    }
}
