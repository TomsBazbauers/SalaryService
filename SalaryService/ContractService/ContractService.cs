namespace Employee_Salary_Service
{
    public class ContractService : IContractService
    {
        private List<EmployeeContract> _employeeContracts = new();
        private readonly IContractValidator _contractValidator;

        public ContractService(IContractValidator contractValidators, List<EmployeeContract> employeeContracts)
        {
            _employeeContracts = employeeContracts;
            _contractValidator = contractValidators;
        }

        public List<EmployeeContract> EmployeeContracts => _employeeContracts;

        public bool AddContract(Employee employee, DateTime contractStart)
        {
            bool isAdded = false;

            if (_contractValidator.IsValid(contractStart) && _contractValidator.IsNotDuplicate(_employeeContracts, employee))
            {
                _employeeContracts.Add(new EmployeeContract(employee.Id, contractStart));
                isAdded = true;
            }

            return isAdded;
        }

        public bool CloseContract(int employeeId, DateTime contractEnd)
        {
            bool isClosed = false;
            var contractToClose = GetActiveContract(employeeId);

            if (_contractValidator.HasValidCloseDate(contractToClose.ContractStartDate, contractEnd) 
                && _contractValidator.IsFound(_employeeContracts, employeeId))
            {
                contractToClose.ContractEndDate = contractEnd;
                isClosed = true;
            }

            return isClosed;
        }

        public EmployeeContract GetActiveContract(int employeeId)
        {
            return _employeeContracts.First(contract => contract.EmployeeId == employeeId && contract.ContractEndDate == DateTime.MinValue);
        }
    }
}