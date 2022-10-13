namespace SalaryServiceCompany
{
    public interface ICompany
    {
        // Name of the company.
        string Name { get; }

        // List of the employees that are working for the company.
        Employee[] Employees { get; }

        // Adds new employee from the given date. Employee Id must be unique.
        void AddEmployee(Employee employee, DateTime contractStartDate);

        // Remove employee from the company at the given date.
        void RemoveEmployee(int employeeId, DateTime contractEndDate);

        // Report worked time at given day and time. 
        void ReportHours(int employeeId, DateTime dateAndTime, int hours, int minutes);

        // Get a full report for each employee where data is available for each month. Assume that there is no overtime.
        EmployeeMonthlyReport[] GetMonthlyReport(DateTime periodStartDate, DateTime periodEndDate);
    }
}