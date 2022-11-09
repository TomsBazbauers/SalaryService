namespace SalaryService
{
    public class EmployeeValidator : IEmployeeValidator
    {
        public decimal minWage = 5;

        public decimal MinWage { get; set; }

        public bool HasValidSalary(Employee employee)
        {
            return employee.HourlySalary >= minWage
                ? true : throw new InvalidSalaryInfoException(employee.HourlySalary.ToString());
        }

        public bool HasValidInfo(Employee employee)
        {
            return !string.IsNullOrEmpty(employee.FullName) && employee.FullName.Trim().Any(Char.IsWhiteSpace) && employee.Id > 0
                ? true : throw new InvalidEmployeeIdException(String.Join(", ", employee.FullName, employee.Id));
        }
    }
}