using Employee_Salary_Service;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests
{
    public class EmployeeMonthlyReportTests
    {
        [Fact]
        public void CreateReport_Input_PropertiesSetAsExpected()
        {
            //Arrange
            var id = 101;
            var periodStartDate = DateTime.Now;
            var periodEndDate = DateTime.Now.AddDays(-5);
            var salary = 120.5m;
            var dateCreated = periodEndDate.Date;

            //Act
            var actual = new EmployeeMonthlyReport(id, periodStartDate, periodEndDate, salary);

            //Assert
            actual.EmployeeId.Should().Be(id);
            actual.Year.Should().Be(periodStartDate.Year);
            actual.Month.Should().Be(periodStartDate.Month);
            actual.Salary.Should().Be(salary);
            actual.DateCreated.Should().Be(dateCreated);
        }
    }
}