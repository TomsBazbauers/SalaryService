namespace SalaryService
{
    public class DuplicateEmployeeException : Exception
    {
        public DuplicateEmployeeException(string info) : base($"[An employee with properties: '{info}' is already registered in the system]")
        {}
    }
}