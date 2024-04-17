using Domain.Entities;
namespace Application.Contracts.Repos
{
    public interface ISharedAreaVisitRepo : IBaseRepo<SharedAreaVisit>
    {
        Task<List<SharedAreaVisit>> GetCheckedInClientsByBranch(Guid branchId);
        Task<SharedAreaVisit> GetVisitClientAreaPricingIncluded(Guid visitId);
        Task<bool> IsClientAlreadyIn(Guid? clientId);
    }
}
