using Employee_Salary_Service.Exceptions;

namespace Employee_Salary_Service
{
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> _registeredEmployees = new();
        private readonly IContractService _contractService;
        private readonly IEmployeeValidator _employeeValidator;

        public List<Employee> Employees => _registeredEmployees;

        public EmployeeService(List<Employee> registered, IEmployeeValidator employeeValidator, IContractService contractService)
        {
            _registeredEmployees = registered;
            _employeeValidator = employeeValidator;
            _contractService = contractService;
        }

        public void Register(Employee employee, DateTime contractStartDate)
        {
            if (_employeeValidator.HasValidSalary(employee)
                && _employeeValidator.HasValidInfo(employee)
                && _contractService.AddContract(employee, contractStartDate))
            {
                _registeredEmployees.Add(employee);
            }
        }

        public void Unregister(int employeeId, DateTime contractEndDate)
        {
            if (IsFound(employeeId)
                && _contractService.CloseContract(employeeId, contractEndDate))
            {
                _registeredEmployees.Remove(GetById(employeeId));
            }
        }

        public bool IsFound(int employeeId)
        {
            return _registeredEmployees.Any(employee => employee.Id == employeeId)
                ? true : throw new EmployeeNotFoundException(employeeId.ToString());
        }

        public Employee GetById(int id)
        {
            return IsFound(id)
                ? _registeredEmployees.First(employee => employee.Id == id) : throw new EmployeeNotFoundException(id.ToString());
        }
    }
}