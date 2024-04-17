using Application.Contracts.Repos;
using Domain.Entities;

namespace Persistence.Implementation.Repos
{
    public class AreaTypeRepo : BaseRepo<AreaType>, IAreaTypeRepo
    {
        public AreaTypeRepo(AppDbContext context) : base(context)
        {
        }
    }
}
