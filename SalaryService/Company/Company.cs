namespace SalaryService
{
    public class Company : ICompany
    {
        private IEmployeeService _employeeService;
        private IReportService _reportService;

        public string Name { get; set; }

        public Employee[] Employees => _employeeService.Employees.ToArray();

        public Company(string name, IEmployeeService employeeService, IReportService reportService)
        {
            Name = name;
            _employeeService = employeeService;
            _reportService = reportService;
        }

        public void AddEmployee(Employee employee, DateTime contractStartDate)
        {
            _employeeService.Register(employee, contractStartDate);
        }

        public EmployeeMonthlyReport[] GetMonthlyReport(DateTime periodStartDate, DateTime periodEndDate)//
        {
            return _reportService.GetMonthly(periodStartDate, periodEndDate, _employeeService.Employees);
        }

        public void RemoveEmployee(int employeeId, DateTime contractEndDate)
        {
            _reportService.PromptReport(_employeeService.GetById(employeeId), contractEndDate);
            _employeeService.Unregister(employeeId, contractEndDate);
        }

        public void ReportHours(int employeeId, DateTime dateAndTime, int hours, int minutes)
        {
            if (_employeeService.IsFound(employeeId))
            {
                _reportService.AddReport(employeeId, dateAndTime, hours, minutes);
            }
        }
    }
}