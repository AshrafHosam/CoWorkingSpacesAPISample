using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Insights.Queries.DashboardSummary
{
    public class DashboardSummaryQuery : IRequest<ApiResponse<DashboardSummaryQueryResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }
    }
}
