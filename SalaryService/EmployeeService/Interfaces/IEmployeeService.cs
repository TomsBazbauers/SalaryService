namespace SalaryService
{
    public interface IEmployeeService
    {
        List<Employee> Employees { get; }

        void Register(Employee employee, DateTime contractStartDate);

        void Unregister(int employeeId, DateTime contractEndDate);

        bool IsFound(int employeeId);

        public Employee GetById(int id);
    }
}