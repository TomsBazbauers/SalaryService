using Employee_Salary_Service;
using Employee_Salary_Service.Exceptions;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests.ReportServiceTests
{
    public class ReportServiceTests
    {
        private ReportService _sut;
        private ISalaryCalculator _calculator;
        private IReportValidator _reportValidator;
        private List<EmployeeDailyReport> _dailyReports;
        private List<EmployeeMonthlyReport> _monthlyReports;

        public ReportServiceTests()
        {
            _calculator = new SalaryCalculator();
            _reportValidator = new ReportValidator();
            _dailyReports = new List<EmployeeDailyReport>();
            _monthlyReports = new List<EmployeeMonthlyReport>();
            _sut = new ReportService(_reportValidator, _calculator, _dailyReports, _monthlyReports);
        }

        [Fact]
        public void AddReport_AllInputsValid_ReportAdded()
        {
            //Arrange
            var id = 101;
            var date = DateTime.Now;
            var hours = 7;
            var minutes = 30;

            //Act
            _sut.AddReport(id, date, hours, minutes);

            //Arrange
            _dailyReports.Count.Should().Be(1);
            _dailyReports[0].EmployeeId.Should().Be(id);
            _dailyReports[0].Date.Should().Be(date);
            _dailyReports[0].HoursWorked.Should().Be(hours + minutes / 60m);
        }

        [Fact]
        public void AddReport_IdInputInvalid_ThrowsException()
        {
            //Arrange
            var id = 0;
            var date = DateTime.Now;
            var hours = 7;
            var minutes = 0;

            //Act
            Action action = () => _sut.AddReport(id, date, hours, minutes);

            //Assert
            action.Should().Throw<InvalidEmployeeIdException>()
                .WithMessage($"[Invalid or missing employee information. Check info: '{id}' to solve this problem]");
        }

        [Fact]
        public void AddReport_HourInputInvalid_ThrowsException()
        {
            //Arrange
            var id = 111;
            var date = DateTime.Now;
            var hours = 15;
            var minutes = 0;

            //Act
            Action action = () => _sut.AddReport(id, date, hours, minutes);

            //Assert
            action.Should().Throw<InvalidHoursException>()
                .WithMessage($"[Hours worked: '{hours}' are ABOVE or BELOW threshold]");
        }

        [Fact]
        public void GetMonthly_InputSmallSize_ReturnExpected()
        {
            //Arrange
            var employeeList = new List<Employee> { new Employee(88, "Patrick Kane", 10), new Employee(8, "Cale Makar", 12) };

            _dailyReports.Add(new EmployeeDailyReport(88, new DateTime(2022, 10, 10), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(88, new DateTime(2022, 10, 11), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(8, new DateTime(2022, 10, 10), 8, 0));
            _dailyReports.Add(new EmployeeDailyReport(8, new DateTime(2022, 10, 11), 8, 0));


            //Act
            var actual = _sut.GetMonthly(new DateTime(2022, 10, 09), DateTime.Now, employeeList);

            //Assert
            actual.Should().BeOfType<EmployeeMonthlyReport[]>();
            actual.Length.Should().Be(2);
            actual[0].DateCreated.Should().Be(DateTime.Now.Date);
            actual[1].DateCreated.Should().Be(DateTime.Now.Date);
            actual[0].Salary.Should().Be(100);
            actual[1].Salary.Should().Be(192);
        }

        [Fact]
        public void PromptReport_InputSmallSize_ReturnExpected()
        {
            //Arrange
            var employee = new Employee(16, "Mitchell Marner", 10);

            _dailyReports.Add(new EmployeeDailyReport(16, new DateTime(2022, 10, 1), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(16, new DateTime(2022, 10, 11), 8, 0));
            _dailyReports.Add(new EmployeeDailyReport(12, new DateTime(2022, 10, 01), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(12, new DateTime(2022, 10, 11), 7, 0));
            _dailyReports.Add(new EmployeeDailyReport(92, new DateTime(2022, 10, 7), 5, 0));

            _monthlyReports.Add(new EmployeeMonthlyReport(12, new DateTime(2022, 09, 01), new DateTime(2022, 09, 10), 100));
            _monthlyReports.Add(new EmployeeMonthlyReport(16, new DateTime(2022, 10, 01), new DateTime(2022, 10, 10), 120));

            //Act
            var actual = _sut.PromptReport(employee, new DateTime(2022, 10, 12));

            //Assert
            actual.Should().BeOfType<EmployeeMonthlyReport>();
            actual.EmployeeId.Should().Be(16);
            actual.Month.Should().Be(10);
            actual.Salary.Should().Be(80);
        }

        [Fact]
        public void FilterIdByDate_InputSmallSize_ReturnExpected()
        {
            //Arrange
            _dailyReports.Add(new EmployeeDailyReport(88, new DateTime(2022, 10, 10), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(88, new DateTime(2022, 10, 11), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(10, new DateTime(2022, 10, 10), 5, 0));
            _dailyReports.Add(new EmployeeDailyReport(12, new DateTime(2022, 10, 10), 5, 0));

            //Act
            var actual = _sut.FilterIdByDate(new DateTime(2022, 10 , 09), DateTime.Now, 88);

            //Assert
            actual.Count.Should().Be(2);
            actual[0].EmployeeId.Should().Be(88);
            actual.Any(r => r.EmployeeId != 88).Should().Be(false);
        }

        [Fact]
        public void GetLastReportDate_InputValid_ReturnExpected()
        {
            //Arrange
            _monthlyReports.Add(new EmployeeMonthlyReport(12, new DateTime(2022, 09, 01), new DateTime(2022, 09, 11), 100));
            _monthlyReports.Add(new EmployeeMonthlyReport(16, new DateTime(2022, 10, 01), new DateTime(2022, 09, 27), 100));
            _monthlyReports.Add(new EmployeeMonthlyReport(12, new DateTime(2022, 03, 01), new DateTime(2022, 10, 10), 100));
            _monthlyReports.Add(new EmployeeMonthlyReport(16, new DateTime(2022, 10, 01), new DateTime(2022, 10, 11), 100));

            //Act
            var actual = _sut.GetLastReportDate();

            //Assert
            actual.Date.Should().Be(new DateTime(2022, 10, 11));
        }
    }
}