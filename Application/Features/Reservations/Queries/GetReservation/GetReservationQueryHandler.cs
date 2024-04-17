using Application.Contracts.Repos;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Reservations.Queries.GetReservation
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ApiResponse<GetReservationQueryResponse>>
    {
        private readonly IReservationRepo _reservationRepo;
        private readonly IMapper _mapper;
        public GetReservationQueryHandler(IReservationRepo reservationRepo, IMapper mapper)
        {
            _reservationRepo = reservationRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetReservationQueryResponse>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepo.GetAsync(request.Id);
            if (reservation == null)
                return ApiResponse<GetReservationQueryResponse>.GetNotFoundApiResponse(error: "Reservation Not Found");

            var response = _mapper.Map<GetReservationQueryResponse>(reservation);
            return ApiResponse<GetReservationQueryResponse>.GetSuccessApiResponse(response);
        }
    }
}
