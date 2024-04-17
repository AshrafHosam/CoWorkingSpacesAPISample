using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.BrandCosts.Commands.DeleteOneTimeExpense
{
    internal class DeleteOneTimeExpenseCommandHandler : IRequestHandler<DeleteOneTimeExpenseCommand, ApiResponse<DeleteOneTimeExpenseCommandResponse>>
    {
        private readonly IOneTimeExpenseRepo _oneTimeExpenseRepository;

        public DeleteOneTimeExpenseCommandHandler(IOneTimeExpenseRepo oneTimeExpenseRepository)
        {
            _oneTimeExpenseRepository = oneTimeExpenseRepository;
        }

        public async Task<ApiResponse<DeleteOneTimeExpenseCommandResponse>> Handle(DeleteOneTimeExpenseCommand request, CancellationToken cancellationToken)
        {
            var oneTimeExpense = await _oneTimeExpenseRepository.GetAsync(request.ExpenseId);
            if (oneTimeExpense is null)
                return ApiResponse<DeleteOneTimeExpenseCommandResponse>.GetNotFoundApiResponse(error: "Expense not found");

            await _oneTimeExpenseRepository.DeleteAsync(oneTimeExpense);

            return ApiResponse<DeleteOneTimeExpenseCommandResponse>.GetNoContentApiResponse();
        }
    }
}
