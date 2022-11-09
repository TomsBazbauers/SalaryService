namespace SalaryService
{
    public class InvalidReportDateException : Exception
    {
        public InvalidReportDateException(string info) : base($"[Invalid date info. Check '{info}' to solve this problem]")
        {}
    }
}
