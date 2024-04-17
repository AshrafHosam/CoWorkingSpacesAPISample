using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using MediatR;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryOneTimeExpenses
{
    internal class GetBrandCostCategoryOneTimeExpensesQueryHandler : IRequestHandler<GetBrandCostCategoryOneTimeExpensesQuery, ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>>
    {
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;
        private readonly IOneTimeExpenseRepo _expenseRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        private readonly IBranchRepo _branchRepo;
        public GetBrandCostCategoryOneTimeExpensesQueryHandler(IBrandCostCategoryRepo brandCostCategoryRepo, IOneTimeExpenseRepo expenseRepo, IUserBrandInternalService userBrandInternalService, IBranchRepo branchRepo)
        {
            _brandCostCategoryRepo = brandCostCategoryRepo;
            _expenseRepo = expenseRepo;
            _userBrandInternalService = userBrandInternalService;
            _branchRepo = branchRepo;
        }

        public async Task<ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>> Handle(GetBrandCostCategoryOneTimeExpensesQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>.GetNotFoundApiResponse(error: "Brand Not Found");

            if (request.CostCategoryId != null && request.CostCategoryId != Guid.Empty)
            {
                var isBrandCostCategoryExist = await _brandCostCategoryRepo.AnyAsync(request.CostCategoryId.Value);
                if (!isBrandCostCategoryExist)
                    return ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>.GetNotFoundApiResponse(error: "Cost Category Not Found");
            }

            if (request.BranchId != null && request.BranchId != Guid.Empty)
            {
                var isBranchExist = await _branchRepo.AnyAsync(request.BranchId.Value);
                if (!isBranchExist)
                    return ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>.GetNotFoundApiResponse(error: "Branch Not Found");
            }

            List<Domain.Entities.OneTimeExpense> oneTimeExpenses;
            long oneTimeExpensesTotalCount;

            if (request.FromDate.HasValue && request.ToDate.HasValue)
            {
                oneTimeExpenses = await _expenseRepo.GetCategoryOneTimeExpenses(request.BrandId, request.BranchId, request.CostCategoryId, request.Page, request.PageSize, request.FromDate.Value, request.ToDate.Value);
                oneTimeExpensesTotalCount = await _expenseRepo.GetCategoryOneTimeExpensesCount(request.BrandId, request.BranchId, request.CostCategoryId, request.FromDate.Value, request.ToDate.Value);
            }
            else
            {
                oneTimeExpenses = await _expenseRepo.GetCategoryOneTimeExpenses(request.BrandId, request.BranchId, request.CostCategoryId, request.Page, request.PageSize);
                oneTimeExpensesTotalCount = await _expenseRepo.GetCategoryOneTimeExpensesCount(request.BrandId, request.BranchId, request.CostCategoryId);
            }

            return ApiResponse<GetBrandCostCategoryOneTimeExpensesQueryResponse>
                .GetSuccessApiResponse(new GetBrandCostCategoryOneTimeExpensesQueryResponse
                {
                    TotalCount = oneTimeExpensesTotalCount,
                    OneTimeExpenses = oneTimeExpenses.Select(a => new OneTimeExpense
                    {
                        Amount = a.Amount,
                        BranchId = a.BranchId,
                        CategoryId = a.BrandCostCategoryId,
                        ExecutionDate = a.TransactionExecutionDate,
                        ExpenseId = a.Id,
                        Name = a.Name
                    }).ToList()
                });
        }
    }
}
