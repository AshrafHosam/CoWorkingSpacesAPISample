using Application.Features.Visits.Commands.CheckOutClient;
using Application.Response;
using MediatR;

namespace Application.Features.Visits.Commands.CheckOutClientsBatch
{
    internal class CheckOutClientsBatchCommandHandler : IRequestHandler<CheckOutClientsBatchCommand, ApiResponse<List<CheckOutClientsBatchCommandResponse>>>
    {
        private readonly IMediator _mediator;

        public CheckOutClientsBatchCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ApiResponse<List<CheckOutClientsBatchCommandResponse>>> Handle(CheckOutClientsBatchCommand request, CancellationToken cancellationToken)
        {
            if ((request.Visits == null || !request.Visits.Any()) || request.Visits.Exists(a => a.VisitId == Guid.Empty))
                return ApiResponse<List<CheckOutClientsBatchCommandResponse>>.GetBadRequestApiResponse("Please provide valid Visit Id");

            var result = new List<CheckOutClientsBatchCommandResponse>();

            foreach (var visit in request.Visits)
                result.Add(new CheckOutClientsBatchCommandResponse
                {
                    TotalAmount = await GetTotal(visit.VisitId, visit.IsSubmitted),
                    VisitId = visit.VisitId
                });

            return ApiResponse<List<CheckOutClientsBatchCommandResponse>>.GetSuccessApiResponse(result);
        }

        private async Task<decimal> GetTotal(Guid visitId, bool isSubmitted)
        {
            return (await _mediator.Send(new CheckOutClientCommand
            {
                IsSubmitted = isSubmitted,
                VisitId = visitId
            }))?.Data?.Total ?? 0;
        }
    }
}
