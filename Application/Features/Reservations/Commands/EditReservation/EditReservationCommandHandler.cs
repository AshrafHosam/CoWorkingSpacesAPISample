using Application.Contracts.Repos;
using Application.Features.Reservations.Commands.Common;
using Application.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Reservations.Commands.EditReservation
{
    public class EditReservationCommandHandler : IRequestHandler<EditReservationCommand, ApiResponse<EditReservationCommandResponse>>
    {
        private readonly IReservationRepo _reservationRepo;
        private readonly IAreaRepo _areaRepo;
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        public EditReservationCommandHandler(IAreaRepo areaRepo, IClientRepo clientRepo, IReservationRepo reservationRepo, IMapper mapper)
        {
            _areaRepo = areaRepo;
            _clientRepo = clientRepo;
            _reservationRepo = reservationRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EditReservationCommandResponse>> Handle(EditReservationCommand request, CancellationToken cancellationToken)
        {
            var isClientExist = await _clientRepo.AnyAsync(request.ClientId);
            if (!isClientExist)
                return ApiResponse<EditReservationCommandResponse>.GetNotFoundApiResponse(error: "Client Not Found");

            var isAreaCriteriaValid = await _areaRepo.IsCreateReservationAreaCriteriaValid(request.AreaId, request.IsHourlyReservation, request.IsDailyReservation, request.IsMonthlyReservation);
            if (!isAreaCriteriaValid)
                return ApiResponse<EditReservationCommandResponse>.GetNotFoundApiResponse(error: "Reservation Criteria Not Valid");

            var areaEntity = await _areaRepo.GetAreaPricingPlansIncluded(request.AreaId);
            var areaTypes = new List<string>
            {
                nameof(AreaTypesEnum.Office),
                nameof(AreaTypesEnum.Room),
                nameof(AreaTypesEnum.Event)
            };

            var isBookableArea = areaTypes.Select(a => a.ToLower()).Contains(areaEntity.AreaType.Name.ToLower());
            if (!isBookableArea)
                return ApiResponse<EditReservationCommandResponse>.GetBadRequestApiResponse(error: "Area Not Bookable");

            var isReservationExist = await _reservationRepo.AnyAsync(request.ReservationId);
            if (!isReservationExist)
                return ApiResponse<EditReservationCommandResponse>.GetNotFoundApiResponse(error: "Reservation Not Found");

            var updatedReservation = new Reservation
            {
                Id = request.ReservationId,
                AreaId = request.AreaId,
                ClientId = request.ClientId,
                IsDailyReservation = request.IsDailyReservation,
                IsHourlyReservation = request.IsHourlyReservation,
                IsMonthlyReservation = request.IsMonthlyReservation,
                Name = request.Name,
                Notes = request.Notes,
                StartDate = request.StartDate.ToUniversalTime(),
                EndDate = request.EndDate.ToUniversalTime()
            };

            updatedReservation.TotalAmount = ReservationAmountCalculator.CalculateReservationTotalAmount(updatedReservation, areaEntity);

            await _reservationRepo.UpdateAsync(updatedReservation);

            return ApiResponse<EditReservationCommandResponse>.GetSuccessApiResponse(new EditReservationCommandResponse
            {
                IsSuccess = true,
                ReservationId = updatedReservation.Id,
                TotalAmount = updatedReservation.TotalAmount
            });
        }
    }
}
