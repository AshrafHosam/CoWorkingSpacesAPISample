using Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.BrandCosts.Commands.DeleteOneTimeExpense
{
    public class DeleteOneTimeExpenseCommand : IRequest<ApiResponse<DeleteOneTimeExpenseCommandResponse>>
    {
        [Required]
        public Guid ExpenseId { get; set; }
    }
}
