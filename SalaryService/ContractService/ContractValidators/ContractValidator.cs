namespace SalaryService
{
    public class ContractValidator : IContractValidator
    {
        public bool HasValidCloseDate(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate ? true : throw new InvalidContractException(String.Join("-", startDate.Date, endDate.Date));
        }

        public bool IsValid(DateTime date)
        {
            return date != DateTime.MinValue ? true : throw new InvalidContractException(date.ToString());
        }

        public bool IsNotDuplicate(List<EmployeeContract> contracts, Employee employee)
        {
            return !contracts.Any(contract => contract.EmployeeId == employee.Id
                && contract.ContractEndDate == DateTime.MinValue)
                ? true : throw new DuplicateContractException(employee.Id.ToString());
        }

        public bool IsFound(List<EmployeeContract> contracts, int employeeId)
        {
            return contracts.Any(contract => contract.EmployeeId == employeeId)
                        ? true : throw new ContractNotFoundException(employeeId.ToString());
        }
    }
}