using Application.Contracts.Identity;
using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ApiResponse<CreateBrandCommandResponse>>
    {
        private readonly IBrandRepo _brandRepo;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public CreateBrandCommandHandler(IBrandRepo brandRepo, IMapper mapper, IClaimService claimService)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<ApiResponse<CreateBrandCommandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            request.OwnerId = Guid.Parse(_claimService.GetUserId());

            var brand = await _brandRepo.GetBrandByOwnerId(request.OwnerId.Value);
            if (brand == null)
            {
                var mappedEntity = _mapper.Map<CreateBrandCommand, Domain.Entities.Brand>(request);
                var createdBrand = await _brandRepo.AddAsync(mappedEntity);

                return ApiResponse<CreateBrandCommandResponse>
                    .GetSuccessApiResponse(new CreateBrandCommandResponse
                    {
                        BrandId = createdBrand.Id
                    });
            }

            return ApiResponse<CreateBrandCommandResponse>
                .GetBadRequestApiResponse(error: "User Already Has a Brand");

        }
    }
}
