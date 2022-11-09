using SalaryService;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests
{
    public class CompanyTests
    {
        private ICompany _sut;
        private IEmployeeService _employeeService;
        private IReportService _reportService;

        public CompanyTests()
        {
            _employeeService =
                new EmployeeService(
                    new List<Employee>(),
                    new EmployeeValidator(),
                    new ContractService(new ContractValidator(), new List<EmployeeContract>()));

            _reportService =
                new ReportService(
                    new ReportValidator(),
                    new SalaryCalculator(),
                    new List<EmployeeDailyReport>(),
                    new List<EmployeeMonthlyReport>());

            _sut = new Company("FastPayService", _employeeService, _reportService);
        }

        [Fact]
        public void AddEmployee_InputValidSingle_EmployeeAdded()
        {
            //Arrange
            var testStartDate = new DateTime(2022, 06, 06);
            var testId = 88;
            var testFullName = "Patrick Kane";
            var testSalary = 15;
            var testEmployee = new Employee(testId, testFullName, testSalary);

            //Act
            _sut.AddEmployee(testEmployee, testStartDate);

            //Assert
            _sut.Employees.Should().BeOfType<Employee[]>();
            _sut.Employees[0].Should().BeSameAs(testEmployee);
            _sut.Employees[0].Id.Should().Be(testId);
            _sut.Employees[0].FullName.Should().Be(testFullName);
            _sut.Employees[0].HourlySalary.Should().Be(testSalary);
        }

        [Fact]
        public void AddEmployee_InputValidMultiple_AllEmployeesAdded()
        {
            //Arrange
            DateTime[] testDates = { new DateTime(2022, 05, 06), new DateTime(2022, 06, 06), new DateTime(2022, 07, 06) };
            int[] testIds = { 88, 16, 97 };
            string[] testNames = { "Patrick Kane", "Mitchell Marner", "Connor McDavid" };
            decimal[] testSalaries = { 10, 12, 15 };
            var testEmployeeA = new Employee(testIds[0], testNames[0], testSalaries[0]);
            var testEmployeeB = new Employee(testIds[1], testNames[1], testSalaries[1]);
            var testEmployeeC = new Employee(testIds[2], testNames[2], testSalaries[2]);

            //Act
            _sut.AddEmployee(testEmployeeA, testDates[0]);
            _sut.AddEmployee(testEmployeeB, testDates[1]);
            _sut.AddEmployee(testEmployeeC, testDates[2]);

            //Assert
            _sut.Employees.Should().BeOfType<Employee[]>();
            _sut.Employees[0].Should().BeSameAs(testEmployeeA);
            _sut.Employees[0].Id.Should().Be(testIds[0]);
            _sut.Employees[0].FullName.Should().Be(testNames[0]);
            _sut.Employees[0].HourlySalary.Should().Be(testSalaries[0]);
            _sut.Employees[1].Should().BeSameAs(testEmployeeB);
            _sut.Employees[1].Id.Should().Be(testIds[1]);
            _sut.Employees[1].FullName.Should().Be(testNames[1]);
            _sut.Employees[1].HourlySalary.Should().Be(testSalaries[1]);
            _sut.Employees[2].Should().BeSameAs(testEmployeeC);
            _sut.Employees[2].Id.Should().Be(testIds[2]);
            _sut.Employees[2].FullName.Should().Be(testNames[2]);
            _sut.Employees[2].HourlySalary.Should().Be(testSalaries[2]);
        }
        [Fact]
        public void AddEmployee_InputValidSalary_ThrowsException()
        {
            //Arrange
            var testStartDate = new DateTime(2022, 06, 06);
            var testId = 64;
            var testFullName = "Auston Matthews";
            var testSalary = 2;
            var testEmployee = new Employee(testId, testFullName, testSalary);

            //Act
            Action action = () => _sut.AddEmployee(testEmployee, testStartDate);

            //Assert
            action.Should().Throw<InvalidSalaryInfoException>($"[Employee salary: '{testEmployee}' is missing or is below legal threshold]");
        }

        [Fact]
        public void AddEmployee_InputInvalidFullName_ThrowsException()
        {
            //Arrange
            var testStartDate = new DateTime(2022, 06, 06);
            var testId = 64;
            var testFullName = "Auston ";
            var testSalary = 12;
            var testEmployee = new Employee(testId, testFullName, testSalary);

            //Act
            Action action = () => _sut.AddEmployee(testEmployee, testStartDate);

            //Assert
            action.Should().Throw<InvalidEmployeeIdException>($"[Invalid or missing employee information. " +
                $"Check info: '{testEmployee}' to solve this problem]");
            _sut.Employees.Length.Should().Be(0);
        }

        [Fact]
        public void AddEmployee_InputInvalidId_ThrowsException()
        {
            //Arrange
            var testStartDate = new DateTime(2022, 06, 06);
            var testId = -64;
            var testFullName = "Auston Matthews";
            var testSalary = 12;
            var testEmployee = new Employee(testId, testFullName, testSalary);

            //Act
            Action action = () => _sut.AddEmployee(testEmployee, testStartDate);

            //Assert
            action.Should().Throw<InvalidEmployeeIdException>($"[Invalid or missing employee information. " +
                $"Check info: '{testEmployee}' to solve this problem]");
            _sut.Employees.Length.Should().Be(0);
        }

        [Fact]
        public void AddEmployee_InputInvalidDuplicate_ThrowsException()
        {
            //Arrange
            DateTime[] testDates = { new DateTime(2022, 05, 06), new DateTime(2022, 06, 06) };
            int[] testIds = { 88, 16 };
            string[] testNames = { "Patrick Kane", "Mitchell Marner" };
            decimal[] testSalaries = { 10, 12 };
            var testEmployeeA = new Employee(testIds[0], testNames[0], testSalaries[0]);
            var testEmployeeB = new Employee(testIds[1], testNames[1], testSalaries[1]);
            var duplicateEmployee = new Employee(testIds[1], testNames[1], testSalaries[1]);

            _sut.AddEmployee(testEmployeeA, testDates[0]);
            _sut.AddEmployee(testEmployeeB, testDates[1]);

            //Act
            Action action = () => _sut.AddEmployee(duplicateEmployee, testDates[0]);

            //Assert
            action.Should().Throw<DuplicateContractException>()
                .WithMessage($"[Contract with the properties: '{duplicateEmployee.Id}' is already registered in the system]");
            _sut.Employees.Where(emp => emp.Id == duplicateEmployee.Id).Count().Should().Be(1);
        }

        [Fact]
        public void GetMonthlyReport_InputValid_ReturnsCorrectReport()
        {
            //Arrange
            var testId = 34;
            var testFullName = "Auston Matthews";
            var testSalary = 12;
            var testH = 7;
            var testM = 30;
            var reportC = 2;
            var testStartDate = new DateTime(2022, 06, 06);
            var testEndDate = new DateTime(2022, 10, 10);
            _employeeService.Employees.Add(new Employee(testId, testFullName, testSalary));
            _reportService.DailyReports.Add(new EmployeeDailyReport(34, new DateTime(2022, 10, 10), testH, testM));
            _reportService.DailyReports.Add(new EmployeeDailyReport(34, new DateTime(2022, 10, 10), testH, testM));

            //Act
            var actual = _sut.GetMonthlyReport(testStartDate, testEndDate);

            //Assert
            actual.Should().BeOfType<EmployeeMonthlyReport[]>();
            actual[0].EmployeeId.Should().Be(testId);
            actual[0].Year.Should().Be(testStartDate.Year);
            actual[0].Month.Should().Be(testStartDate.Month);
            actual[0].Salary.Should().Be(reportC * (testSalary * (testH + testM / 60m)));
        }

        [Fact]
        public void RemoveEmployee_InputValid_CorrectEmployeeRemoved()
        {
            //Arrange
            var testId = 64;
            var testFullName = "Auston Matthews";
            var testSalary = 12;
            var testH = 7;
            var testM = 30;
            var testStartDate = new DateTime(2022, 09, 01);
            var testEndDate = new DateTime(2022, 10, 01);
            var testReport = new EmployeeMonthlyReport(64, testStartDate, testEndDate, 100);
            testReport.DateCreated = testEndDate;

            _sut.AddEmployee(new Employee(testId, testFullName, testSalary), testStartDate);
            _sut.AddEmployee(new Employee(53, "Teddy Blueger", testSalary), testStartDate);
            _reportService.DailyReports.Add(new EmployeeDailyReport(64, new DateTime(2022, 10, 10), testH, testM));
            _reportService.MonthlyReports.Add(testReport);

            //Act
            _sut.RemoveEmployee(64, DateTime.Now);

            //Assert
            _sut.Employees.Length.Should().Be(1);
            _sut.Employees.Any(e => e.Id != testId).Should().BeTrue();
        }

        [Fact]
        public void ReportHours_InputValid_ReportAdded()
        {
            //Arrange
            _employeeService.Employees.Add(new Employee(8, "Cale Makar", 12));
            _employeeService.Employees.Add(new Employee(53, "Teddy Blueger", 12));

            //Act
            _sut.ReportHours(8, DateTime.Now, 5, 0);

            //Assert
            _reportService.DailyReports.Count.Should().Be(1);
            _reportService.DailyReports.Any(r => r.EmployeeId == 8).Should().BeTrue();
            _reportService.DailyReports.Any(r => r.EmployeeId == 53).Should().BeFalse();
        }

        [Fact]
        public void ReportHours_InputInvalidDate_ThrowsException()
        {
            //Arrange
            var testDate = DateTime.Now.AddDays(5);
            _employeeService.Employees.Add(new Employee(8, "Cale Makar", 12));

            //Act
            Action action = () => _sut.ReportHours(8, testDate, 5, 0);

            //Assert
            action.Should().Throw<InvalidReportDateException>()
                .WithMessage($"[Invalid date info. Check '{testDate}' to solve this problem]");
        }

        [Fact]
        public void ReportHours_InputInvalidId_ThrowsException()
        {
            //Arrange
            var testDate = DateTime.Now.AddDays(5);
            var testId = 101;
            _employeeService.Employees.Add(new Employee(8, "Cale Makar", 12));

            //Act
            Action action = () => _sut.ReportHours(testId, testDate, 5, 0);

            //Assert
            action.Should().Throw<EmployeeNotFoundException>()
                .WithMessage($"[Employee with the Id: '{testId}' is not found]");
        }
    }
}