namespace SalaryService
{
    public class DuplicateReportException : Exception
    {
        public DuplicateReportException(string id) : base($"[Invalid properties. Employee: '{id}' report for the date has been already received]")
        {}
    }
}