namespace Employee_Salary_Service
{
    public class EmployeeMonthlyReport
    {
        public EmployeeMonthlyReport(int employeeId, DateTime periodStartDate, DateTime periodEndDate, decimal salary)
        {
            EmployeeId = employeeId;
            Year = periodStartDate.Year;
            Month = periodStartDate.Month;
            Salary = salary;
            DateCreated = periodEndDate.Date;
        }

        public int EmployeeId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Salary { get; set; }

        public DateTime DateCreated { get; set; }
    }
}