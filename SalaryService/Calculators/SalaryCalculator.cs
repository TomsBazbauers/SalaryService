namespace SalaryService
{
    public class SalaryCalculator : ISalaryCalculator
    {
        public decimal GetSalary(List<EmployeeDailyReport> dailyReports, decimal hourlySalary)
        {
            decimal monthlySalary = Math.Round(dailyReports
                .Select(report => report.HoursWorked * hourlySalary).ToList().Sum(), 2);

            return monthlySalary;
        }
    }
}