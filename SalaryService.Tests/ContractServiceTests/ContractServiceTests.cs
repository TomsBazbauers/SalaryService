using SalaryService;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests
{
    public class ContractServiceTests
    {
        private ContractService _sut;
        private List<EmployeeContract> _employeeContracts;
        private IContractValidator _contractValidator;

        public ContractServiceTests()
        {
            _employeeContracts = new List<EmployeeContract>();
            _contractValidator = new ContractValidator();
            _sut = new ContractService(_contractValidator, _employeeContracts);
        }

        [Fact]
        public void AddContract_InputValid_CorrectContractAdded()
        {
            //Arrange
            var employee = new Employee(88, "Patrick Kane", 10);
            var contractStart = new DateTime(2022, 10, 01);

            //Act
            var returnValue = _sut.AddContract(employee, contractStart);

            //Assert
            returnValue.Should().BeTrue();
            _employeeContracts.Any(c => c.EmployeeId == employee.Id).Should().BeTrue();
            _employeeContracts
                .Any(c => c.EmployeeId == employee.Id
                && c.ContractStartDate == contractStart
                && c.ContractEndDate == DateTime.MinValue).Should().BeTrue();
        }

        [Fact]
        public void AddContract_InputInvalidId_ThrowsException()
        {
            //Arrange
            var employee = new Employee(88, "Patrick Kane", 10);
            var contractStart = DateTime.MinValue;

            //Act
            Action action = () => _sut.AddContract(employee, contractStart);

            //Assert
            action.Should().Throw<InvalidContractException>()
                .WithMessage($"[Invalid contract info. Check '{contractStart}' to solve this problem]");
        }

        [Fact]
        public void AddContract_InputInvalidDuplicate_ThrowsException()
        {
            //Arrange
            var employee = new Employee(88, "Patrick Kane", 10);
            var testEmployee = new Employee(88, "Patrick Kane", 12);
            var contractStart = new DateTime(2022, 10, 01);

            _employeeContracts.Add(new EmployeeContract(testEmployee.Id, contractStart));

            //Act
            Action action = () => _sut.AddContract(employee, contractStart);

            //Assert
            action.Should().Throw<DuplicateContractException>()
                .WithMessage($"[Contract with the properties: '{employee.Id}' is already registered in the system]");
        }
        [Fact]
        public void CloseContract_InputValid_CorrectContractClosed()
        {
            //Arrange
            var testId = 33;
            _employeeContracts.Add(new EmployeeContract(1, new DateTime(2022, 10, 10)));
            _employeeContracts.Add(new EmployeeContract(2, new DateTime(2022, 10, 01)));
            _employeeContracts.Add(new EmployeeContract(testId, new DateTime(2022, 09, 10)));

            //Act
            var returnValue = _sut.CloseContract(testId, DateTime.Now);

            //Assert
            returnValue.Should().BeTrue();
            _employeeContracts
                .Any(c => c.EmployeeId == testId
                && c.ContractEndDate == null).Should().BeFalse();
        }

        [Fact]
        public void CloseContract_InputInvalidDates_ThrowsException()
        {
            //Arrange
            var testId = 33;
            var testDateEnd = new DateTime(2021, 10, 10);
            var testDateStart = new DateTime(2022, 10, 10);
            _employeeContracts.Add(new EmployeeContract(testId, testDateStart));

            //Act
            Action action = () => _sut.CloseContract(testId, testDateEnd);

            //Assert
            action.Should().Throw<InvalidContractException>()
                .WithMessage($"[Invalid contract info. Check '{string.Join("-", testDateStart.Date, testDateEnd.Date)}' to solve this problem]");
        }

        [Fact]
        public void GetActiveContract_InputValid_ReturnsCorrectContract()
        {
            //Arrange
            var testId = 2;
            _employeeContracts.Add(new EmployeeContract(12, new DateTime(2022, 10, 10)));
            _employeeContracts.Add(new EmployeeContract(testId, new DateTime(2022, 10, 01)));

            //Act
            var returnValue = _sut.GetActiveContract(testId);

            //Assert
            returnValue.EmployeeId.Should().Be(testId);
            returnValue.ContractEndDate.Should().Be(DateTime.MinValue);
        }
    }
}