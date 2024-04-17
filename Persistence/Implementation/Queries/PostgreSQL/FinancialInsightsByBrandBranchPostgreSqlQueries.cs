namespace Persistence.Implementation.Queries.PostgreSQL
{
    internal static class FinancialInsightsByBrandBranchPostgreSqlQueries
    {
        public static string OneTimeExpenseQueryByBrandBranch = @"SELECT
    EXTRACT(YEAR FROM ote.""TransactionExecutionDate"") AS Year,
    EXTRACT(MONTH FROM ote.""TransactionExecutionDate"") AS Month,
    SUM(""Amount"") AS TotalAmount
FROM public.""OneTimeExpenses"" AS ote
	JOIN public.""BrandCostCategories"" as bcc
		ON ote.""BrandCostCategoryId"" = bcc.""Id""
		AND bcc.""BrandId"" = @brandId
	JOIN public.""Branches"" as b
		ON b.""Id"" = ote.""BranchId""
		AND b.""Id"" = @branchId
WHERE ote.""IsDeleted"" = false
GROUP BY EXTRACT(YEAR FROM ote.""TransactionExecutionDate""),
EXTRACT(MONTH FROM ote.""TransactionExecutionDate"");";


        public static string VisitsIncomeQueryByBrandBranch = @"SELECT
    EXTRACT(YEAR FROM sav.""CheckOutStamp"") AS Year,
    EXTRACT(MONTH FROM sav.""CheckOutStamp"") AS Month,
    SUM(""TotalAmount"") AS TotalAmount
FROM public.""SharedAreaVisits"" AS sav
	JOIN public.""Branches"" as b
	ON b.""Id"" = sav.""BranchId""
	AND b.""Id"" = @branchId
	AND b.""BrandId"" = @brandId
WHERE sav.""IsDeleted"" = false 
	AND sav.""CheckOutStamp"" IS NOT NULL 
	AND sav.""TotalAmount"" IS NOT NULL
GROUP BY EXTRACT(YEAR FROM sav.""CheckOutStamp""),
EXTRACT(MONTH FROM sav.""CheckOutStamp"");";

        public static string Reservations_Horly_Daily_IncomeQueryByBrandBranch = @"SELECT
    EXTRACT(YEAR FROM r.""EndDate"") AS Year,
    EXTRACT(MONTH FROM r.""EndDate"") AS Month,
    SUM(""TotalAmount"") AS TotalAmount
FROM public.""Reservations"" AS r
	JOIN public.""Areas"" as a
		ON a.""Id"" = r.""AreaId""
	JOIN public.""Branches"" as b
		ON b.""Id"" = a.""BranchId""
		AND b.""Id"" = @branchId
		AND b.""BrandId"" = @brandId
WHERE r.""IsDeleted"" = false 
	AND r.""EndDate"" IS NOT NULL 
	AND r.""TotalAmount"" IS NOT NULL
	AND (r.""IsHourlyReservation"" = true OR r.""IsDailyReservation"" = true)
GROUP BY EXTRACT(YEAR FROM r.""EndDate""),
EXTRACT(MONTH FROM r.""EndDate"");";

        public static string RecurringExpenseQueryByBrandBranch = @"SELECT
    EXTRACT(YEAR FROM rea.""TransactionExecutionDate"") AS Year,
    EXTRACT(MONTH FROM rea.""TransactionExecutionDate"") AS Month,
    SUM(""Amount"") AS TotalAmount
FROM public.""RecurringExpenseAmounts"" AS rea
	JOIN public.""RecurringExpenses"" as re
		ON rea.""RecurringExpenseId"" = re.""Id""
	JOIN public.""BrandCostCategories"" as bcc
		ON bcc.""Id"" = re.""BrandCostCategoryId""
		AND bcc.""BrandId"" = @brandId
	JOIN public.""Branches"" as b
		ON b.""Id"" = re.""BranchId""
		AND b.""Id"" = @branchId
WHERE rea.""IsDeleted"" = false
GROUP BY EXTRACT(YEAR FROM rea.""TransactionExecutionDate""),
EXTRACT(MONTH FROM rea.""TransactionExecutionDate"");";
    }
}
