using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IReservationRepo : IBaseRepo<Reservation>
    {
        Task<List<Reservation>> GetAllReservationsFromToDate(DateTimeOffset fromDate, DateTimeOffset toDate, Guid branchId);
        Task<List<Reservation>> GetReservationsByClientId(Guid clientId);
        Task<bool> IsValidReservation(Guid? reservationId, DateTimeOffset startTime, DateTimeOffset endDate, Guid areaId);
    }
}
