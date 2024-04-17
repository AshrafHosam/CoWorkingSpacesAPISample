using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Reservations.Queries.GetClientReservations
{
    public class GetClientReservationsQueryHandler : IRequestHandler<GetClientReservationsQuery, ApiResponse<List<GetClientReservationsQueryResponse>>>
    {
        private readonly IReservationRepo _reservationRepo;
        private readonly IMapper _mapper;
        private readonly IClientRepo _clientRepo;
        public GetClientReservationsQueryHandler(IReservationRepo reservationRepo, IMapper mapper, IClientRepo clientRepo)
        {
            _reservationRepo = reservationRepo;
            _mapper = mapper;
            _clientRepo = clientRepo;
        }

        public async Task<ApiResponse<List<GetClientReservationsQueryResponse>>> Handle(GetClientReservationsQuery request, CancellationToken cancellationToken)
        {
            var isClientExist = await _clientRepo.AnyAsync(request.ClientId);
            if (!isClientExist)
                return ApiResponse<List<GetClientReservationsQueryResponse>>.GetNotFoundApiResponse(error: "Client Not Found");

            var reservations = await _reservationRepo.GetReservationsByClientId(request.ClientId);
            if (!reservations.Any())
                return ApiResponse<List<GetClientReservationsQueryResponse>>.GetNotFoundApiResponse(error: "Not Reservations Found For This Client");

            var response = _mapper.Map<List<GetClientReservationsQueryResponse>>(reservations);
            return ApiResponse<List<GetClientReservationsQueryResponse>>.GetSuccessApiResponse(response);
        }
    }
}
