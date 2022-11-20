# Salary Service

### Description:
Simple salary service for calculating employee salaries based on reported time. 
- Unit tested with XUnit and FluentAssertions.

### Brief:
- A company can have any number of employees.
- New employees can be recruited, and existing employees can leave a company.
- The same employee can return to the company.
- An employee is reporting worked hours daily.
- You must calculate the monthly salary for each employee.
---
- Use TDD approach.
- Think about OOP design patterns and S.O.L.I.D. principles
- In case of error, throw different type of exceptions for each situation
- No need for UI and database
- It is not allowed to change the given code.
---
### We are giving some interfaces and classes:


```
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

    // Get a full report for each employee where data is available for each month. 
    // Assume that there is no overtime.
    EmployeeMonthlyReport[] GetMonthlyReport(DateTime periodStartDate, DateTime periodEndDate);
}

public class Employee
{
    // Unique ID of the employee.
    public int Id { get; set; }

    // Employee full name.
    public string FullName { get; set; }

    // Hourly salary of worked full hour. Use proportion for time smaller than 1 hour.
    public decimal HourlySalary { get; set; }
}

public class EmployeeMonthlyReport
{
    public int EmployeeId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public decimal Salary { get; set; }
}
```
