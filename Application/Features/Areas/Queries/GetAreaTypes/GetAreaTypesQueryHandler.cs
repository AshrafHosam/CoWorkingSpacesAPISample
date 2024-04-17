using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Areas.Queries.GetAreaTypes
{
    public class GetAreaTypesQueryHandler : IRequestHandler<GetAreaTypesQuery, ApiResponse<List<GetAreaTypesQueryResponse>>>
    {
        private readonly IAreaRepo _areaRepo;
        private readonly IMapper _mapper;

        public GetAreaTypesQueryHandler(IAreaRepo areaRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAreaTypesQueryResponse>>> Handle(GetAreaTypesQuery request, CancellationToken cancellationToken)
        {
            return ApiResponse<List<GetAreaTypesQueryResponse>>.GetSuccessCacheableApiResponse(_mapper.Map<List<GetAreaTypesQueryResponse>>((await _areaRepo.GetAreaTypesAsync())));
        }
    }
}
