namespace Employee_Salary_Service.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(string id) : base($"[Employee with the Id: '{id}' is not found]")
        {}
    }
}