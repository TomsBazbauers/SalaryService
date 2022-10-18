namespace SalaryService
{
    public class InvalidSalaryInfoException : Exception
    {
        public InvalidSalaryInfoException(string salary) : base($"[Employee salary: '{salary}' is missing or is below legal threshold]")
        {}
    }
}