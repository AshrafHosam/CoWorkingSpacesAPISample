using Domain.Entities;

namespace Application.Contracts.Repos
{
    public interface IBrandRepo : IBaseRepo<Brand>
    {
        Task<Brand> GetBrandByOwnerId(Guid ownerId);
    }
}
