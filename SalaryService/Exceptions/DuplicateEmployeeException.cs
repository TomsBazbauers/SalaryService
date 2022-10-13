namespace Employee_Salary_Service.Exceptions
{
    public class DuplicateEmployeeException : Exception
    {
        public DuplicateEmployeeException(string info) : base($"[An employee with properties: '{info}' is already registered in the system]")
        {}
    }
}