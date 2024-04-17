using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    internal class ReservationRepo : BaseRepo<Reservation>, IReservationRepo
    {
        public ReservationRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Reservation>> GetAllReservationsFromToDate(DateTimeOffset fromDate, DateTimeOffset toDate, Guid branchId)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(a => a.Client)
                .Where(x => (x.StartDate >= fromDate && x.EndDate <= toDate) && x.Area.BranchId == branchId)
                .OrderBy(x => x.StartDate)
                .ToListAsync();
        }
        public async Task<List<Reservation>> GetReservationsByClientId(Guid clientId)
        {
            return await _context.Reservations
                .Where(x => x.ClientId == clientId)
                .OrderBy(x => x.StartDate)
                .ToListAsync();
        }
        public async Task<bool> IsValidReservation(Guid? reservationId, DateTimeOffset startTime, DateTimeOffset endDate, Guid areaId)
        {
            var overlappingReservations = await _context.Reservations
                .FilterIf(reservationId.HasValue, r => r.Id != reservationId.Value)
                .Where(r =>
                    r.AreaId == areaId &&
                    (startTime < r.EndDate && endDate >= r.StartDate))
                .ToListAsync();

            return !overlappingReservations.Any();
        }
    }
}
