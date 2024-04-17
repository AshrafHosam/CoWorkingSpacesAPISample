using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.BrandCosts.Queries.GetBrandCostCategoryRecurringExpenses
{
    internal class GetBrandCostCategoryRecurringExpensesQueryHandler : IRequestHandler<GetBrandCostCategoryRecurringExpensesQuery, ApiResponse<GetBrandCostCategoryRecurringExpensesQueryResponse>>
    {
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;
        private readonly IRecurringExpenseRepo _recurringExpenseRepo;
        private readonly IUserBrandInternalService _userBrandInternalService;
        public GetBrandCostCategoryRecurringExpensesQueryHandler(IBrandCostCategoryRepo brandCostCategoryRepo, IRecurringExpenseRepo recurringExpenseRepo, IUserBrandInternalService userBrandInternalService)
        {
            _brandCostCategoryRepo = brandCostCategoryRepo;
            _recurringExpenseRepo = recurringExpenseRepo;
            _userBrandInternalService = userBrandInternalService;
        }

        public async Task<ApiResponse<GetBrandCostCategoryRecurringExpensesQueryResponse>> Handle(GetBrandCostCategoryRecurringExpensesQuery request, CancellationToken cancellationToken)
        {
            var userBrand = await _userBrandInternalService.GetLoggedInUserBrand();
            if (!userBrand.IsSuccessStatusCode || (userBrand.IsSuccessStatusCode && userBrand.Data.Id != request.BrandId))
                return ApiResponse<GetBrandCostCategoryRecurringExpensesQueryResponse>.GetNotFoundApiResponse(error: "User Brand Not Found");

            var isBrandCostCategoryExist = await _brandCostCategoryRepo.AnyAsync(request.CostCategoryId);
            if (!isBrandCostCategoryExist)
                return ApiResponse<GetBrandCostCategoryRecurringExpensesQueryResponse>.GetNotFoundApiResponse(error: "Cost Category Not Found");

            var recurringExpenses = await _recurringExpenseRepo.GetCategoryRecurringExpenses(request.BrandId, request.CostCategoryId, request.Page, request.PageSize);

            var recurringExpensesTotalCount = await _recurringExpenseRepo.GetCategoryRecurringExpensesCount(request.BrandId, request.CostCategoryId);

            return ApiResponse<GetBrandCostCategoryRecurringExpensesQueryResponse>.GetSuccessApiResponse(MapRecurringExpensesDTO(recurringExpenses, recurringExpensesTotalCount));
        }

        private GetBrandCostCategoryRecurringExpensesQueryResponse MapRecurringExpensesDTO(List<RecurringExpense> recurringExpenses, long recurringExpensesTotalCount)
        {
            return new GetBrandCostCategoryRecurringExpensesQueryResponse
            {
                TotalCount = recurringExpensesTotalCount,
                RecurringExpenses = recurringExpenses.Select(a => new RecurringExpenseDTO
                {
                    BranchId = a.BranchId,
                    CategoryId = a.BrandCostCategoryId,
                    ExpenseId = a.Id,
                    Name = a.Name,
                    NumberOfOccurs = a.RecurringExpenseAmounts.Count,
                    RecurringTimeSpanUnit = a.RecurringTimeSpanUnit.ToString(),
                    RecurringExpenseAmounts = a.RecurringExpenseAmounts.Select(b => new RecurringExpenseAmountDTO
                    {
                        Amount = b.Amount,
                        ExecutionDate = b.TransactionExecutionDate,
                        ExpenseAmountId = b.Id
                    }).ToList()
                }).ToList()
            };
        }
    }
}
