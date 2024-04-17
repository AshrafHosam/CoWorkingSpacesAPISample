using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.BrandCosts.Commands.EditOneTimeExpense
{
    internal class EditOneTimeExpenseCommandHandler : IRequestHandler<EditOneTimeExpenseCommand, ApiResponse<EditOneTimeExpenseCommandResponse>>
    {
        private readonly IOneTimeExpenseRepo _oneTimeExpenseRepository;
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepository;
        private readonly IBranchRepo _branchRepo;
        public EditOneTimeExpenseCommandHandler(IOneTimeExpenseRepo oneTimeExpenseRepository, IBrandCostCategoryRepo brandCostCategoryRepository, IBranchRepo branchRepo)
        {
            _oneTimeExpenseRepository = oneTimeExpenseRepository;
            _brandCostCategoryRepository = brandCostCategoryRepository;
            _branchRepo = branchRepo;
        }

        public async Task<ApiResponse<EditOneTimeExpenseCommandResponse>> Handle(EditOneTimeExpenseCommand request, CancellationToken cancellationToken)
        {
            if (request.BranchId.HasValue)
            {
                var isBranchExist = await _branchRepo.AnyAsync(request.BranchId.Value);
                if (!isBranchExist)
                    return ApiResponse<EditOneTimeExpenseCommandResponse>.GetNotFoundApiResponse(error: "Branch not found");
            }

            var isCategoryExist = await _brandCostCategoryRepository.AnyAsync(request.CategoryId);
            if (!isCategoryExist)
                return ApiResponse<EditOneTimeExpenseCommandResponse>.GetNotFoundApiResponse(error: "Cost Category not found");

            var expense = await _oneTimeExpenseRepository.GetAsync(request.ExpenseId);
            if (expense is null)
                return ApiResponse<EditOneTimeExpenseCommandResponse>.GetNotFoundApiResponse(error: "Expense not found");

            expense.BrandCostCategoryId = request.CategoryId;
            expense.Amount = request.Amount;
            expense.Name = request.Name;
            expense.BranchId = request.BranchId;
            expense.TransactionExecutionDate = request.ExecutionDate;

            await _oneTimeExpenseRepository.UpdateAsync(expense);

            return ApiResponse<EditOneTimeExpenseCommandResponse>.GetSuccessApiResponse(new EditOneTimeExpenseCommandResponse
            {
                ExpenseId = expense.Id,
                ExecutionDate = expense.TransactionExecutionDate,
                Amount = expense.Amount,
                Name = expense.Name,
                BranchId = expense.BranchId,
                CategoryId = expense.BrandCostCategoryId
            });
        }
    }
}
