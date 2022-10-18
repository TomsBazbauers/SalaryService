namespace SalaryService
{
    public interface IReportService
    {
        List<EmployeeDailyReport> DailyReports { get; }

        List<EmployeeMonthlyReport> MonthlyReports { get; }

        void AddReport(int employeeId, DateTime dateAndTime, int hours, int minutes);

        EmployeeMonthlyReport[] GetMonthly(DateTime periodStartDate, DateTime periodEndDate, List<Employee> employees);

        EmployeeMonthlyReport PromptReport(Employee employee, DateTime contractEndDate);

        List<EmployeeDailyReport> FilterIdByDate(DateTime periodStartDate, DateTime periodEndDate, int id);

        public DateTime GetLastReportDate();
    }
}
