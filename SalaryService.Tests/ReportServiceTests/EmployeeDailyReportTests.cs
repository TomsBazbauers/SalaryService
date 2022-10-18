using SalaryService;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests
{
    public class EmployeeDailyReportTests
    {
        [Fact]
        public void CreateReport_Input_PropertiesSetAsExpected()
        {
            //Arrange
            var id = 101;
            var date = DateTime.Now;
            var hours = 7;
            var minutes = 35;
            var hoursWorked = 7 + 35 / 60m;

            //Act
            var actual = new EmployeeDailyReport(id, date, hours, minutes);

            //Assert
            actual.EmployeeId.Should().Be(id);
            actual.Date.Should().Be(date);
            actual.HoursWorked.Should().Be(hoursWorked);
        }
    }
}
