using Application.Contracts.Repos;
using Application.Features.Clients.Queries.GetClient;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, ApiResponse<List<GetAllReservationsQueryResponse>>>
    {
        private readonly IReservationRepo _reservationRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IMapper _mapper;
        public GetAllReservationsQueryHandler(IReservationRepo reservationRepo, IBranchRepo branchRepo, IMapper mapper)
        {
            _reservationRepo = reservationRepo;
            _branchRepo = branchRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAllReservationsQueryResponse>>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var isBranchExist = await _branchRepo.AnyAsync(request.BranchId);
            if (!isBranchExist)
                return ApiResponse<List<GetAllReservationsQueryResponse>>.GetNotFoundApiResponse(error: "Branch Not Exist");

            var reservations = await _reservationRepo.GetAllReservationsFromToDate(request.FromDate.ToUniversalTime(), request.ToDate.ToUniversalTime(), request.BranchId);

            var response = reservations.Select(a => new GetAllReservationsQueryResponse
            {
                AreaId = a.AreaId,
                ClientId = a.ClientId,
                EndDate = a.EndDate,
                Id = a.Id,
                IsAllDay = a.IsDailyReservation || a.IsMonthlyReservation,
                Name = a.Name,
                Notes = a.Notes,
                StartDate = a.StartDate,
                TotalAmount = a.TotalAmount,
                IsHourlyReservation = a.IsHourlyReservation,
                IsMonthlyReservation = a.IsMonthlyReservation,
                IsDailyReservation = a.IsDailyReservation,
                Client = _mapper.Map<ClientDto>(a.Client)
            }).ToList();

            return ApiResponse<List<GetAllReservationsQueryResponse>>.GetSuccessApiResponse(response);
        }
    }
}
