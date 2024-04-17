using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Visits.Queries.GetNonCheckedInClients
{
    internal class GetNonCheckedInClientsQueryHandler : IRequestHandler<GetNonCheckedInClientsQuery, ApiResponse<List<GetNonCheckedInClientsQueryResponse>>>
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        public GetNonCheckedInClientsQueryHandler(IClientRepo clientRepo, IMapper mapper)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetNonCheckedInClientsQueryResponse>>> Handle(GetNonCheckedInClientsQuery request, CancellationToken cancellationToken)
        {
            var nonCheckedInClients = await _clientRepo.GetNonCheckedInClientsByBranch(request.BrandId, request.SearchText, request.PageNumber, request.PageSize);

            var resultDto = _mapper.Map<List<GetNonCheckedInClientsQueryResponse>>(nonCheckedInClients);

            return ApiResponse<List<GetNonCheckedInClientsQueryResponse>>.GetSuccessApiResponse(resultDto);
        }
    }
}
