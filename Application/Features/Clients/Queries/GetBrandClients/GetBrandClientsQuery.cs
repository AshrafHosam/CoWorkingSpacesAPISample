using Application.Features.Clients.Queries.GetClient;
using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Clients.Queries.GetBrandClients
{
    public class GetBrandClientsQuery : IRequest<ApiResponse<GetClientQueryResponse>>
    {
        [Required]
        public Guid BrandId { get; set; }
        [Range(1.0, int.MaxValue)]
        public int Page { get; set; } = 1;
        [Range(1.0, 200.0)]
        public int PageSize { get; set; } = 10;
    }
}
