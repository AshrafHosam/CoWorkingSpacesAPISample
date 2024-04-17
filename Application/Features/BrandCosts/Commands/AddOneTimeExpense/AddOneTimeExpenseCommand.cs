using Application.Response;
using MediatR;

namespace Application.Features.BrandCosts.Commands.AddOneTimeExpense
{
    public class AddOneTimeExpenseCommand : IRequest<ApiResponse<AddOneTimeExpenseCommandResponse>>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTimeOffset ExecutionDate { get; set; } = DateTimeOffset.Now;
    }
}
