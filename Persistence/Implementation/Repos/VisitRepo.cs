using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    public class SharedAreaVisitRepo : BaseRepo<SharedAreaVisit>, ISharedAreaVisitRepo
    {
        public SharedAreaVisitRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<List<SharedAreaVisit>> GetCheckedInClientsByBranch(Guid branchId)
        {
            return await _context.SharedAreaVisits
                .Include(a => a.Client)
                .Include(a => a.Area)
                .Include(a => a.CustomServices)
                .Where(a => a.BranchId == branchId
                && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date
                && !a.CheckOutStamp.HasValue)
                .ToListAsync();
        }

        public async Task<bool> IsClientAlreadyIn(Guid? clientId)
        {
            return await _context.SharedAreaVisits
                .AnyAsync(a => a.ClientId == clientId && a.CheckOutStamp == null && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date);
        }

        private static readonly Func<AppDbContext, Guid, Task<SharedAreaVisit>> GetVisitWithIncludesQuery = async (db, visitId)
            => (await db.SharedAreaVisits
                .Include(a => a.Client)
                    .ThenInclude(a => a.Visits)
                .Include(a => a.Area)
                    .ThenInclude(a => a.SharedAreaPricingPlanModel)
                .FirstOrDefaultAsync(a => a.Id == visitId)) ?? null as SharedAreaVisit;

        public async Task<SharedAreaVisit> GetVisitClientAreaPricingIncluded(Guid visitId)
        {
            return await GetVisitWithIncludesQuery(_context, visitId);
        }
    }
}
