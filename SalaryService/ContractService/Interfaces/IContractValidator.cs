namespace SalaryService
{
    public interface IContractValidator
    {
        bool IsValid(DateTime firstDate);

        bool HasValidCloseDate(DateTime startDate, DateTime endDate);

        bool IsNotDuplicate(List<EmployeeContract> contracts, Employee employee);

        bool IsFound(List<EmployeeContract> contracts, int employeeId);
    }
}