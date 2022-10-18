namespace SalaryService
{
    public class InvalidHoursException : Exception
    {
        public InvalidHoursException(string hours) : base($"[Hours worked: '{hours}' are ABOVE or BELOW threshold]")
        {}
    }
}