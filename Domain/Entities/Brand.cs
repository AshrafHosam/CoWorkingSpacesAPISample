using Domain.Common;

namespace Domain.Entities
{
    public class Brand : BaseEntity
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }

        public virtual IEnumerable<Branch> Branches { get; set; }

    }
}
