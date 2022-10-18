using SalaryService;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests
{
    public class SalaryCalculatorTests
    {
        private SalaryCalculator _sut;
        private List<EmployeeDailyReport> _reports;

        public SalaryCalculatorTests()
        {
            _sut = new SalaryCalculator();
            _reports = new List<EmployeeDailyReport>();
            _reports.Add(new EmployeeDailyReport(10, DateTime.Now, 7, 30));
            _reports.Add(new EmployeeDailyReport(10, DateTime.Now, 7, 30));
            _reports.Add(new EmployeeDailyReport(10, DateTime.Now, 4, 0));
            _reports.Add(new EmployeeDailyReport(10, DateTime.Now, 7, 0));
            _reports.Add(new EmployeeDailyReport(10, DateTime.Now, 2, 30));
            _reports.Add(new EmployeeDailyReport(10, DateTime.Now, 8, 0));
        }

        [Fact]
        public void GetSalary_InputSalaryDecimal_ReturnsExpectedSalary()
        {
            //Arrange
            var testSalary = 8.5m;
            var expected = Math.Round(_reports.Select(r => r.HoursWorked * testSalary).ToList().Sum(), 2);

            //Act
            var actual = _sut.GetSalary(_reports, testSalary);

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetSalary_InputSalaryInteger_ReturnsExpectedSalary()
        {
            //Arrange
            var testSalary = 8;
            var expected = Math.Round(_reports.Select(r => r.HoursWorked * testSalary).ToList().Sum(), 2);

            //Act
            var actual = _sut.GetSalary(_reports, testSalary);

            //Assert
            actual.Should().Be(expected);
        }
    }
}