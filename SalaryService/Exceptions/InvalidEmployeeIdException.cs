namespace SalaryService
{
    public class InvalidEmployeeIdException : Exception
    {
        public InvalidEmployeeIdException(string info) : base($"[Invalid or missing employee information. Check info: '{info}' to solve this problem]")
        {}
    }
}