using FluentValidation;

namespace Application.Features.Areas.Queries.GetArea
{
    public class GetAreaQueryValidator : AbstractValidator<GetAreaQuery>
    {
        public GetAreaQueryValidator()
        {
            RuleFor(a=>a.AreaId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
