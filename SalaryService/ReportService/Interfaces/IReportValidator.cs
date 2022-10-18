namespace SalaryService
{
    public interface IReportValidator
    {
        public decimal MaxHours { get; set; }

        public bool IsValid(int hours, int minutes);

        public bool HasValidId(int id);

        public bool HasValidDate(DateTime date);

        public bool IsUnique(int id, DateTime date, List<EmployeeDailyReport> reports);
    }
}
