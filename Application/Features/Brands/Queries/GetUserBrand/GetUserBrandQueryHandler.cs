using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries.GetUserBrand
{
    public class GetUserBrandQueryHandler : IRequestHandler<GetUserBrandQuery, ApiResponse<GetUserBrandQueryResponse>>
    {
        private readonly IBrandRepo _brandRepo;
        private readonly IMapper _mapper;
        public GetUserBrandQueryHandler(IBrandRepo brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetUserBrandQueryResponse>> Handle(GetUserBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepo.GetBrandByOwnerId(request.UserId);

            if (brand == null)
                return ApiResponse<GetUserBrandQueryResponse>.GetNotFoundApiResponse(error: "No Brand Found For This User");

            return ApiResponse<GetUserBrandQueryResponse>.GetSuccessCacheableApiResponse(_mapper.Map<GetUserBrandQueryResponse>(brand));
        }
    }
}
