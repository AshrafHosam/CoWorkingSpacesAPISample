using Application.Features.Clients.Queries.GetClient;
using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IClientRepo : IBaseRepo<Client>
    {
        Task<List<Client>> GetNonCheckedInClientsByBranch(Guid brandId, string searchText, int page, int pageSize);
        Task<Client> SearchAsync(Guid brandId, string email, string mobileNumber);
        Task<HashSet<string>> FilterExistingEmails(Guid brandId, List<string> emails);
        Task<List<ClientDto>> GetBrandClientsPaginated(Guid brandId, int page, int pageSize);
        Task<List<ClientDto>> SearchForClient(string searchText, Guid brandId, int page, int pageSize);
        Task<int> GetClientsCount(Guid brandId, string searchText = null);
        Task<HashSet<string>> FilterExistingPhoneNumbers(Guid brandId, List<string> phoneNumbers);
        Task<Client> GetBrandClient(Guid brandId, Guid clientId);
        Task DeleteAllClients(Guid brandId);
    }
}
