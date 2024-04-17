using Application.Contracts.Repos;
using Application.Features.Reservations.Commands.Common;
using Application.Response;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reservations.Commands.AddReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ApiResponse<CreateReservationCommandResponse>>
    {
        private readonly IReservationRepo _reservationRepo;
        private readonly IAreaRepo _areaRepo;
        private readonly IClientRepo _clientRepo;
        public CreateReservationCommandHandler(IAreaRepo areaRepo, IClientRepo clientRepo, IReservationRepo reservationRepo)
        {
            _areaRepo = areaRepo;
            _clientRepo = clientRepo;
            _reservationRepo = reservationRepo;
        }

        public async Task<ApiResponse<CreateReservationCommandResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var isAreaValid = await _areaRepo.IsCreateReservationAreaCriteriaValid(request.AreaId, request.IsHourlyReservation, request.IsDailyReservation, request.IsMonthlyReservation);
            if (!isAreaValid)
                return ApiResponse<CreateReservationCommandResponse>.GetNotFoundApiResponse(error: "Area not valid for reservation");

            var areaEntity = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            var areaTypes = new List<string>
            {
                nameof(AreaTypesEnum.Office),
                nameof(AreaTypesEnum.Room),
                nameof(AreaTypesEnum.Event)
            };

            var isBookableArea = areaTypes.Select(a => a.ToLower()).Contains(areaEntity.AreaType.Name.ToLower());
            if (!isBookableArea)
                return ApiResponse<CreateReservationCommandResponse>.GetBadRequestApiResponse(error: "Area Not Bookable");

            var isClientExist = await _clientRepo.AnyAsync(request.ClientId);
            if (!isClientExist)
                return ApiResponse<CreateReservationCommandResponse>.GetNotFoundApiResponse(error: "Client Not Found");

            var isAreaAvailable = await _reservationRepo.IsValidReservation(null,
                request.StartDate.ToUniversalTime(),
                request.EndDate.ToUniversalTime(),
                request.AreaId);

            if (!isAreaAvailable)
                return ApiResponse<CreateReservationCommandResponse>.GetBadRequestApiResponse(error: "Area Not Available");

            var reservation = new Reservation
            {
                AreaId = request.AreaId,
                ClientId = request.ClientId,

                IsDailyReservation = request.IsDailyReservation,
                IsHourlyReservation = request.IsHourlyReservation,
                IsMonthlyReservation = request.IsMonthlyReservation,

                StartDate = request.StartDate.ToUniversalTime(),
                EndDate = request.EndDate.ToUniversalTime(),

                Notes = request.Notes,
                Name = request.Name
            };

            reservation.TotalAmount = ReservationAmountCalculator.CalculateReservationTotalAmount(reservation, areaEntity);

            var createdReservation = await _reservationRepo.AddAsync(reservation);
            if (createdReservation is null)
                return ApiResponse<CreateReservationCommandResponse>.GetBadRequestApiResponse(error: "Reservation Not Created");

            return ApiResponse<CreateReservationCommandResponse>.GetSuccessApiResponse(new CreateReservationCommandResponse
            {
                ReservationId = createdReservation.Id,
                TotalAmount = reservation.TotalAmount
            });
        }
    }
}
