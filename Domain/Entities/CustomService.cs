using Domain.Common;

namespace Domain.Entities
{
    public class CustomService : BaseEntity
    {
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
    }
}
