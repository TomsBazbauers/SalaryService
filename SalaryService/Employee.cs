namespace SalaryServiceCompany
{
    public class Employee
    {
        // Unique ID of the employee.
        public int Id { get; set; }

        // Employee full name.
        public string FullName { get; set; }

        // Hourly salary of worked full hour. Use proportion for time smaller than 1 hour.
        public decimal HourlySalary { get; set; }
    }
}