using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Queries.GetClient
{
    public class GetClientQuery : IRequest<ApiResponse<GetClientQueryResponse>>
    {
        public Guid? Id { get; set; }
        public string SearchText { get; set; }
        [Range(1.0, int.MaxValue)]
        public int Page { get; set; } = 1;
        [Range(1.0, 200.0)]
        public int PageSize { get; set; } = 10;
    }
}
