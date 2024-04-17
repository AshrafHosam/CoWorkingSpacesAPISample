using Application.Contracts.Repos;
using Application.Response;
using MediatR;

namespace Application.Features.BrandCosts.Commands.AddOneTimeExpense
{
    internal class AddOneTimeExpenseCommandHandler : IRequestHandler<AddOneTimeExpenseCommand, ApiResponse<AddOneTimeExpenseCommandResponse>>
    {
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IOneTimeExpenseRepo _expenseRepo;
        public AddOneTimeExpenseCommandHandler(IBrandCostCategoryRepo brandCostCategoryRepo, IBranchRepo branchRepo, IOneTimeExpenseRepo expenseRepo)
        {
            _brandCostCategoryRepo = brandCostCategoryRepo;
            _branchRepo = branchRepo;
            _expenseRepo = expenseRepo;
        }

        public async Task<ApiResponse<AddOneTimeExpenseCommandResponse>> Handle(AddOneTimeExpenseCommand request, CancellationToken cancellationToken)
        {
            var isBrandCategoryExist = await _brandCostCategoryRepo.AnyAsync(request.CategoryId);
            if (!isBrandCategoryExist)
                return ApiResponse<AddOneTimeExpenseCommandResponse>.GetNotFoundApiResponse(error: "Cost Category Not Found");

            if (request.BranchId.HasValue && !request.BranchId.Equals(Guid.Empty))
            {
                var isBranchCategoryExist = await _branchRepo.AnyAsync(request.BranchId.Value);
                if (!isBranchCategoryExist)
                    return ApiResponse<AddOneTimeExpenseCommandResponse>.GetNotFoundApiResponse(error: "Branch Not Found");
            }

            var expense = await _expenseRepo.AddAsync(new Domain.Entities.OneTimeExpense
            {
                BranchId = request.BranchId.HasValue ? request.BranchId : null,
                Amount = request.Amount,
                BrandCostCategoryId = request.CategoryId,
                Name = request.Name,
                TransactionExecutionDate = request.ExecutionDate
            });

            return ApiResponse<AddOneTimeExpenseCommandResponse>.GetSuccessApiResponse(
                new AddOneTimeExpenseCommandResponse
                {
                    ExpenseId = expense.Id,
                    ExecutionDate = expense.TransactionExecutionDate,
                    Amount = expense.Amount,
                    CategoryId = expense.BrandCostCategoryId,
                    BranchId = expense.BranchId,
                    Name = expense.Name
                });
        }
    }
}
