namespace SalaryService
{
    public interface ISalaryCalculator
    {
        public decimal GetSalary(List<EmployeeDailyReport> dailyReports, decimal hourlySalary);
    }
}
