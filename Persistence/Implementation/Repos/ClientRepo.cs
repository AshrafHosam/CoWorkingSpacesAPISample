using Application.Contracts.Repos;
using Application.Features.Clients.Queries.GetClient;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence.Implementation.Queries.PostgreSQL;

namespace Persistence.Implementation.Repos
{
    public class ClientRepo : BaseRepo<Client>, IClientRepo
    {
        public ClientRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ClientDto>> GetBrandClientsPaginated(Guid brandId, int page, int pageSize)
        {
            return await _context.Clients
                .AsNoTracking()
                .Where(a => a.BrandId == brandId)
                .OrderByDescending(a => a.CreatedDate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(a => new ClientDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                    MobileNumber = a.MobileNumber,
                    Interests = a.Interests,
                    Source = a.Source,
                    ProfessionalTitle = a.ProfessionalTitle,
                    VisitedHours = Math.Round(a.Visits.Where(b => b.CheckOutStamp.HasValue).Select(b => b.CheckOutStamp - b.CheckInStamp).Sum(b => b.Value.TotalHours), 2, MidpointRounding.AwayFromZero),
                    NumberOfVisits = a.Visits.Count(b => b.CheckOutStamp.HasValue),
                    NumberOfReservations = a.Reservations.Count
                })
                .ToListAsync();
        }

        public async Task<List<ClientDto>> SearchForClient(string searchText, Guid brandId, int page, int pageSize)
        {
            return await _context.Clients
                .AsNoTracking()
                .Where(a => a.BrandId == brandId)
                .FilterIf(!string.IsNullOrEmpty(searchText),
                a => EF.Functions.ILike(a.Name, $"%{searchText}%") ||
                EF.Functions.ILike(a.Email, $"%{searchText}%") ||
                EF.Functions.ILike(a.MobileNumber, $"%{searchText}%"))
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(a => new ClientDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                    MobileNumber = a.MobileNumber,
                    Interests = a.Interests,
                    Source = a.Source,
                    ProfessionalTitle = a.ProfessionalTitle,
                    VisitedHours = Math.Round(a.Visits.Where(b => b.CheckOutStamp.HasValue).Select(b => b.CheckOutStamp - b.CheckInStamp).Sum(b => b.Value.TotalHours), 2, MidpointRounding.AwayFromZero),
                    NumberOfVisits = a.Visits.Count(b => b.CheckOutStamp.HasValue),
                    NumberOfReservations = a.Reservations.Count
                })
                .ToListAsync();
        }

        public async Task<List<Client>> GetNonCheckedInClientsByBranch(Guid brandId, string searchText, int page, int pageSize)
        {
            var checkedInClientsIds = await _context.SharedAreaVisits
                .AsNoTracking()
                .Where(a => a.Client.BrandId == brandId
                && a.CheckInStamp.Date == DateTimeOffset.UtcNow.Date
                && !a.CheckOutStamp.HasValue)
                .Select(a => a.ClientId)
                .ToListAsync();

            return await _context.Clients
                .AsNoTracking()
                .Where(a => a.BrandId == brandId &&
                !checkedInClientsIds.Contains(a.Id))
                .FilterIf(!string.IsNullOrEmpty(searchText),
                a => (EF.Functions.ILike(a.Email, $"%{searchText}%")
                || EF.Functions.ILike(a.Name, $"%{searchText}%")
                || EF.Functions.ILike(a.MobileNumber, $"%{searchText}%")))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Client> SearchAsync(Guid brandId, string email, string mobileNumber)
        {
            return await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.BrandId == brandId
                && (a.Email.Contains(email.ToLower())
                || a.MobileNumber.Contains(mobileNumber.ToLower())));
        }

        public async Task<HashSet<string>> FilterExistingEmails(Guid brandId, List<string> emails)
        {
            var existingEmail = await _context.Clients
                .AsNoTracking()
                .Where(a => a.BrandId == brandId
                && emails.Contains(a.Email))
                .Select(a => a.Email)
                .Distinct()
                .ToListAsync();

            return emails.Except(existingEmail).ToHashSet();

        }

        public async Task<int> GetClientsCount(Guid brandId, string searchText = null)
        {
            return await _context.Clients
                .AsNoTracking()
                .Where(a => a.BrandId == brandId)
                .FilterIf(!string.IsNullOrEmpty(searchText),
                a => EF.Functions.ILike(a.Name, $"%{searchText}%") ||
                EF.Functions.ILike(a.Email, $"%{searchText}%") ||
                EF.Functions.ILike(a.MobileNumber, $"%{searchText}%"))
                .CountAsync();
        }

        public async Task<HashSet<string>> FilterExistingPhoneNumbers(Guid brandId, List<string> phoneNumbers)
        {
            var existingPhoneNumbers = await _context.Clients
                .AsNoTracking()
                .Where(a => a.BrandId == brandId
                && phoneNumbers.Contains(a.MobileNumber))
                .Select(a => a.MobileNumber)
                .Distinct()
                .ToListAsync();

            return phoneNumbers.Except(existingPhoneNumbers).ToHashSet();
        }

        public async Task<Client> GetBrandClient(Guid brandId, Guid clientId) => await _context.Clients
                .FirstOrDefaultAsync(a => a.BrandId == brandId && a.Id == clientId);

        public async Task DeleteAllClients(Guid brandId)
            => await _context
            .Database
            .ExecuteSqlRawAsync(ClientsPostgreSql.DeleteAllClients,
                new NpgsqlParameter("@brandId", brandId));
    }
}
