using Application.Contracts.Factories;
using Application.Contracts.Repos;
using Application.Contracts.Strategies;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.BrandCosts.Commands.AddRecurringExpense
{
    internal class AddRecurringExpenseCommandHandler : IRequestHandler<AddRecurringExpenseCommand, ApiResponse<AddRecurringExpenseCommandResponse>>
    {
        private readonly IBrandCostCategoryRepo _brandCostCategoryRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IRecurringExpenseRepo _recurringExpenseRepo;
        private readonly IStrategyFactory _strategyFactory;
        public AddRecurringExpenseCommandHandler(IBrandCostCategoryRepo brandCostCategoryRepo, IBranchRepo branchRepo, IRecurringExpenseRepo recurringExpenseRepo, IStrategyFactory strategyFactory)
        {
            _brandCostCategoryRepo = brandCostCategoryRepo;
            _branchRepo = branchRepo;
            _recurringExpenseRepo = recurringExpenseRepo;
            _strategyFactory = strategyFactory;
        }

        public async Task<ApiResponse<AddRecurringExpenseCommandResponse>> Handle(AddRecurringExpenseCommand request, CancellationToken cancellationToken)
        {
            var isBrandCategoryExist = await _brandCostCategoryRepo.AnyAsync(request.CategoryId);
            if (!isBrandCategoryExist)
                return ApiResponse<AddRecurringExpenseCommandResponse>.GetNotFoundApiResponse(error: "Cost Category Not Found");

            if (request.BranchId.HasValue && !request.BranchId.Equals(Guid.Empty))
            {
                var isBranchCategoryExist = await _branchRepo.AnyAsync(request.BranchId.Value);
                if (!isBranchCategoryExist)
                    return ApiResponse<AddRecurringExpenseCommandResponse>.GetNotFoundApiResponse(error: "Branch Not Found");
            }

            var recurringExpenseEntity = ExecuteRecurringExpenseStrategy(request);

            if (recurringExpenseEntity is null)
                return ApiResponse<AddRecurringExpenseCommandResponse>.GetBadRequestApiResponse(error: "Recurring Expense Not Created");

            await _recurringExpenseRepo.AddAsync(recurringExpenseEntity);

            return ApiResponse<AddRecurringExpenseCommandResponse>
                .GetSuccessApiResponse(new AddRecurringExpenseCommandResponse
                {
                    RecurringExpenseId = recurringExpenseEntity.Id
                });
        }

        private RecurringExpense ExecuteRecurringExpenseStrategy(AddRecurringExpenseCommand request)
        {
            var strategyModel = new RecurringExpenseStrategyModel
            {
                Amount = request.Amount,
                BranchId = request.BranchId,
                CategoryId = request.CategoryId,
                Name = request.Name,
                NumberOfOccurs = request.NumberOfOccurs,
                StartDate = request.StartDate,
                RecurringTimeSpanUnit = request.RecurringTimeSpanUnit,
                SelectedDates = request.SelectedDates
            };

            return _strategyFactory
                .GetRecurringExpenseStrategy(request)
                .Execute(strategyModel);
        }
    }
}
