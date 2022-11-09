namespace SalaryService
{
    public interface IEmployeeValidator
    {
        bool HasValidInfo(Employee employee);

        bool HasValidSalary(Employee employee);
    }
}
