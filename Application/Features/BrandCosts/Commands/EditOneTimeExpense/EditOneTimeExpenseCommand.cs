using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCosts.Commands.EditOneTimeExpense
{
    public class EditOneTimeExpenseCommand : IRequest<ApiResponse<EditOneTimeExpenseCommandResponse>>
    {
        [Required]
        public Guid ExpenseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTimeOffset ExecutionDate { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Guid? BranchId { get; set; }
    }
}
