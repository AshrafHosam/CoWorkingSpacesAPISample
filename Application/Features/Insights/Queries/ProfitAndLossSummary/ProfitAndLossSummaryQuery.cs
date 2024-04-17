using Application.Response;
using MediatR;

namespace Application.Features.Insights.Queries.ProfitAndLossSummary
{
    public class ProfitAndLossSummaryQuery : IRequest<ApiResponse<ProfitAndLossSummaryQueryResponse>>
    {
        public Guid BrandId { get; set; }
        public Guid? BranchId { get; set; }
    }
}
