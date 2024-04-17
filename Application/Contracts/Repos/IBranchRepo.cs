using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IBranchRepo : IBaseRepo<Branch>
    {
        Task<List<Branch>> GetBranchesByBrandId(Guid brandId);
        Task<Branch> GetAsync(Guid branchId, Guid brandId);
    }
}
