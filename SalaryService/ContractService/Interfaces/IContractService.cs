namespace SalaryService
{
    public interface IContractService
    {
        List<EmployeeContract> EmployeeContracts { get; }

        bool AddContract(Employee employee, DateTime contractStart);

        bool CloseContract(int employeeId, DateTime contractEnd);

        EmployeeContract GetActiveContract(int employeeId);
    }
}