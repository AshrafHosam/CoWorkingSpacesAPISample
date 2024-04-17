namespace Application.Features.Insights.Queries.ProfitAndLossSummary
{
    public class ProfitAndLossSummaryQueryResponse
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Profit
        {
            get
            {
                return TotalIncome - TotalExpenses;
            }
        }
        public MostProfitableAreaDto MostProfitableArea { get; set; }
        public TopClientDto TopClient { get; set; }

        public List<FinancialChartDto> FinancialChartData { get; set; }
    }

    public class FinancialChartDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }

    public class TopClientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MostProfitableAreaDto
    {
        public string BranchName { get; set; }
        public string AreaName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}