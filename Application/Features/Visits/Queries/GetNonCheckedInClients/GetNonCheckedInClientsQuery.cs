using Application.Response;
using MediatR;

namespace Application.Features.Visits.Queries.GetNonCheckedInClients
{
    public class GetNonCheckedInClientsQuery : IRequest<ApiResponse<List<GetNonCheckedInClientsQueryResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public Guid BrandId { get; set; }
        public string SearchText { get; set; }
    }
}
