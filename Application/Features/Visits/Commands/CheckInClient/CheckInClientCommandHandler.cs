using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Issentialz.Application.Features.Visits.Commands.CheckInClient
{
    public class CheckInClientCommandHandler : IRequestHandler<CheckInClientCommand, ApiResponse<CheckInClientCommandResponse>>
    {
        private readonly ISharedAreaVisitRepo _visitRepo;
        private readonly IClientRepo _clientRepo;

        public CheckInClientCommandHandler(ISharedAreaVisitRepo visitRepo, IClientRepo clientRepo)
        {
            _visitRepo = visitRepo;
            _clientRepo = clientRepo;
        }

        public async Task<ApiResponse<CheckInClientCommandResponse>> Handle(CheckInClientCommand request, CancellationToken cancellationToken)
        {
            if (request.ClientId.HasValue)
            {
                var clientExist = await _clientRepo.AnyAsync(request.ClientId.Value);
                if (!clientExist)
                    return ApiResponse<CheckInClientCommandResponse>.GetNotFoundApiResponse(error: "Client Not Found");


                var isClientAlreadyIn = await _visitRepo.IsClientAlreadyIn(request.ClientId);
                if (isClientAlreadyIn)
                    return ApiResponse<CheckInClientCommandResponse>.GetBadRequestApiResponse(error: "Client Already Checked In");


                var newVisit = await _visitRepo.AddAsync(new Domain.Entities.SharedAreaVisit
                {
                    ClientId = request.ClientId.Value,
                    AreaId = request.AreaId,
                    BranchId = request.BranchId,
                    CheckInStamp = DateTimeOffset.UtcNow
                });

                return ApiResponse<CheckInClientCommandResponse>
                    .GetSuccessApiResponse(new CheckInClientCommandResponse
                    {
                        ClientId = newVisit.ClientId,
                        VisitId = newVisit.Id
                    });
            }

            var newClient = await _clientRepo.AddAsync(new Domain.Entities.Client
            {
                MobileNumber = request.ClientPhoneNumber,
                Email = request.ClientEmail,
                Name = request.ClientName
            });
            var newVisitEntity = await _visitRepo.AddAsync(new Domain.Entities.SharedAreaVisit
            {
                ClientId = newClient.Id,
                AreaId = request.AreaId,
                BranchId = request.BranchId,
                CheckInStamp = DateTimeOffset.UtcNow
            });

            return ApiResponse<CheckInClientCommandResponse>
                .GetSuccessApiResponse(new CheckInClientCommandResponse
                {
                    ClientId = newVisitEntity.ClientId,
                    VisitId = newVisitEntity.Id
                });
        }
    }
}
