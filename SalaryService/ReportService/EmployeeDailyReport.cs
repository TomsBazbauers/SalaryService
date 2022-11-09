namespace SalaryService
{
    public struct EmployeeDailyReport
    {
        public EmployeeDailyReport(int employeeId, DateTime date, int hours, int minutes)
        {
            EmployeeId = employeeId;
            Date = date;
            HoursWorked = hours + minutes / 60m;
        }

        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public decimal HoursWorked { get; set; }
    }
}