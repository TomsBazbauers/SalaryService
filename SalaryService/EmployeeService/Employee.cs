namespace Employee_Salary_Service
{
    public class Employee
    {
        public Employee(int id, string fullName, decimal hourlySalary)
        {
            Id = id;
            FullName = fullName;
            HourlySalary = hourlySalary;
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public decimal HourlySalary { get; set; }
    }
}