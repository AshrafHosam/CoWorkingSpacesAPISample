using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Areas.Queries.GetArea
{
    public class GetAreaQuery : IRequest<ApiResponse<GetAreaQueryResponse>>
    {
        [Required]
        public Guid AreaId { get; set; }
    }
}
