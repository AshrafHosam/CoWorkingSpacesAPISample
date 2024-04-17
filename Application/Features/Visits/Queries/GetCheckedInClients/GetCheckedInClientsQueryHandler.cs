using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Issentialz.Application.Features.Visits.Queries.GetCheckedInClients
{
    public class GetCheckedInClientsQueryHandler : IRequestHandler<GetCheckedInClientsQuery, ApiResponse<List<GetCheckedInClientsQueryResponse>>>
    {
        private readonly ISharedAreaVisitRepo _visitRepo;
        private readonly IMapper _mapper;
        public GetCheckedInClientsQueryHandler(ISharedAreaVisitRepo visitRepo, IMapper mapper)
        {
            _visitRepo = visitRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetCheckedInClientsQueryResponse>>> Handle(GetCheckedInClientsQuery request, CancellationToken cancellationToken)
        {
            var checkedInClients = await _visitRepo.GetCheckedInClientsByBranch(request.BranchId);

            var result = _mapper.Map<List<GetCheckedInClientsQueryResponse>>(checkedInClients);

            return ApiResponse<List<GetCheckedInClientsQueryResponse>>.GetSuccessApiResponse(result);
        }
    }
}
