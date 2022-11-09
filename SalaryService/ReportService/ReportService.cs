namespace SalaryService
{
    public class ReportService : IReportService
    {
        private readonly ISalaryCalculator _calculator;
        private readonly IReportValidator _reportValidator;
        private List<EmployeeDailyReport> _dailyReports;
        private List<EmployeeMonthlyReport> _monthlyReports;

        public List<EmployeeDailyReport> DailyReports => _dailyReports;

        public List<EmployeeMonthlyReport> MonthlyReports => _monthlyReports;

        public ReportService(IReportValidator reportValidator, ISalaryCalculator calculator, 
            List<EmployeeDailyReport> dailyReports, List<EmployeeMonthlyReport> monthlyReports)
        {
            _reportValidator = reportValidator;
            _calculator = calculator;
            _dailyReports = dailyReports;
            _monthlyReports = monthlyReports;
        }

        public void AddReport(int employeeId, DateTime dateAndTime, int hours, int minutes)
        {
            if (_reportValidator.IsValid(hours, minutes) 
                && _reportValidator.HasValidId(employeeId)
                && _reportValidator.HasValidDate(dateAndTime)
                && _reportValidator.IsUnique(employeeId, dateAndTime, _dailyReports))
            {
                _dailyReports.Add(new EmployeeDailyReport(employeeId, dateAndTime, hours, minutes));
            }
        }

        public EmployeeMonthlyReport[] GetMonthly(DateTime periodStartDate, DateTime periodEndDate, List<Employee> employees)
        {
            List<EmployeeMonthlyReport> reports = new();

            foreach (Employee employee in employees)
            {
                var filtered = FilterIdByDate(periodStartDate, periodEndDate, employee.Id);
                var salary = _calculator.GetSalary(filtered, employee.HourlySalary);
                reports.Add(new EmployeeMonthlyReport(employee.Id, periodStartDate, periodEndDate, salary));
            }

            _monthlyReports.AddRange(reports);

            return reports.ToArray();
        }

        public EmployeeMonthlyReport PromptReport(Employee employee, DateTime contractEndDate)
        {
            var startDate = GetLastReportDate();
            var filtered = FilterIdByDate(startDate, contractEndDate, employee.Id);
            var salary = _calculator.GetSalary(filtered, employee.HourlySalary);

            return new EmployeeMonthlyReport(employee.Id, startDate, contractEndDate, salary);
        }

        public List<EmployeeDailyReport> FilterIdByDate(DateTime periodStartDate, DateTime periodEndDate, int id)
        {
            return _dailyReports
                .Where(report => report.Date >= periodStartDate.Date
                && report.Date <= periodEndDate.Date && report.EmployeeId == id).ToList();
        }

        public DateTime GetLastReportDate()
        {
            return _monthlyReports.OrderByDescending(report => report.DateCreated).ToList().First().DateCreated.Date;
        }
    }
}