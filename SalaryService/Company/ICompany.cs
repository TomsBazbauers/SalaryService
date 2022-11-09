namespace SalaryService
{
    public interface ICompany
    {
        string Name { get; }

        Employee[] Employees { get; }

        void AddEmployee(Employee employee, DateTime contractStartDate);

        void RemoveEmployee(int employeeId, DateTime contractEndDate);

        void ReportHours(int employeeId, DateTime dateAndTime, int hours, int minutes);

        EmployeeMonthlyReport[] GetMonthlyReport(DateTime periodStartDate, DateTime periodEndDate);
    }
}
