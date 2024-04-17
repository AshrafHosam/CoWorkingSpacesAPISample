using Application.Response;
using MediatR;

namespace Application.Features.Visits.Commands.CheckOutClientsBatch
{
    public class CheckOutClientsBatchCommand : IRequest<ApiResponse<List<CheckOutClientsBatchCommandResponse>>>
    {
        public List<CheckOutClientsBatchCommandVisitDto> Visits { get; set; } = new();
    }
    public class CheckOutClientsBatchCommandVisitDto
    {
        public Guid VisitId { get; set; }
        public bool IsSubmitted { get; set; } = false;
    }
}
