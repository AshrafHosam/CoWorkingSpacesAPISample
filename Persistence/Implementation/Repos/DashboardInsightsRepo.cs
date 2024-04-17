using Application.Contracts.Repos;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Implementation.Repos
{
    internal class DashboardInsightsRepo : IDashboardInsightsRepo
    {
        private readonly AppDbContext _context;

        public DashboardInsightsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<decimal> GetClientsCount(Guid brandId)
        {
            return await _context.Clients
                .AsNoTracking()
                .Where(a => a.Brand.Id == brandId)
                .CountAsync();
        }
        public async Task<decimal> GetAverageHoursCount(Guid brandId)
        {
            var visits = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Area.Branch.BrandId == brandId && a.CheckOutStamp.HasValue)
                .Select(a => new
                {
                    CheckIn = a.CheckInStamp,
                    CheckOut = a.CheckOutStamp
                })
                .ToListAsync();

            return visits.Any() ? (decimal)visits
                .Where(a => a is not null && a.CheckOut is not null)
                .Average(a => (a.CheckOut - a.CheckIn).Value.TotalHours) : 0;
        }
        public async Task<decimal> GetCheckInCount(Guid brandId)
        {
            return await _context.SharedAreaVisits
                .AsNoTracking()
                .CountAsync(a => a.Branch.BrandId == brandId
                && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date
                && !a.CheckOutStamp.HasValue);
        }
        public async Task<decimal> GetCurrentOccupacy(Guid brandId)
        {
            var sharedAreaIds = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId
                && !a.CheckOutStamp.HasValue
                && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date
                && a.Area.Capacity > 0)
                .Select(a => new
                {
                    a.AreaId,
                    a.Area.Capacity
                })
                .Distinct()
                .ToListAsync();

            var sharedAreasCheckInsCount = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Branch.BrandId == brandId
                && !a.CheckOutStamp.HasValue
                && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date)
                .GroupBy(a => a.AreaId)
                .Select(a => new
                {
                    AreaId = a.Key,
                    CheckInsCount = a.Count()
                })
                .ToListAsync();

            var areaPercentages = sharedAreasCheckInsCount.Select(checkIn =>
            {
                var areaId = checkIn.AreaId;
                var checkInsCount = checkIn.CheckInsCount;

                var areaCapacity = sharedAreaIds.FirstOrDefault(area => area.AreaId == areaId)?.Capacity ?? 0;

                var percentage = (double)checkInsCount / areaCapacity * 100;

                return new
                {
                    AreaId = areaId,
                    Percentage = percentage
                };
            }).ToList();


            if (areaPercentages.Any())
            {
                var averagePercentage = areaPercentages.Average(a => a.Percentage);

                return (decimal)averagePercentage;
            }

            return 0;
        }
    }
}
