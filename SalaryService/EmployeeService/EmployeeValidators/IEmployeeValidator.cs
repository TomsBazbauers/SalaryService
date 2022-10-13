namespace Employee_Salary_Service
{
    public interface IEmployeeValidator
    {
        bool HasValidInfo(Employee employee);

        bool HasValidSalary(Employee employee);
    }
}
