namespace Application.Features.Visits.Commands.CheckOutClientsBatch
{
    public class CheckOutClientsBatchCommandResponse
    {
        public Guid VisitId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}