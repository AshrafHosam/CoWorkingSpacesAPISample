using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, ApiResponse<DeleteReservationCommandResponse>>
    {
        private readonly IReservationRepo _reservationRepo;
        public DeleteReservationCommandHandler(IReservationRepo reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }

        public async Task<ApiResponse<DeleteReservationCommandResponse>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepo.GetAsync(request.Id);
            if (reservation == null)
                return ApiResponse<DeleteReservationCommandResponse>.GetNotFoundApiResponse(error: "Reservation Not Found");

            await _reservationRepo.DeleteAsync(reservation);
            return ApiResponse<DeleteReservationCommandResponse>.GetSuccessApiResponse(new DeleteReservationCommandResponse
            {
                IsSuccess = true
            });
        }
    }
}
