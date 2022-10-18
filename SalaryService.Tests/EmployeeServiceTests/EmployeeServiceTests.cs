using SalaryService;
using FluentAssertions;
using Xunit;

namespace SalaryService.Tests
{
    public class EmployeeServiceTests
    {
        private EmployeeService _sut;
        private List<Employee> _registeredEmployees;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IContractService _contractService;
       
        public EmployeeServiceTests()
        {
            _registeredEmployees = new List<Employee>();
            _employeeValidator = new EmployeeValidator();
            _contractService = new ContractService(new ContractValidator(), new List<EmployeeContract>());
            _sut = new EmployeeService(_registeredEmployees, _employeeValidator, _contractService);
        }

        [Fact]
        public void Register_InputValid_EmployeeRegistered()
        {
            //Arrange
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(88, "Patrick Kane", 12);

            //Act
            _sut.Register(testEmployeeA, new DateTime(2022, 10, 10));
            _sut.Register(testEmployeeB, new DateTime(2022, 10, 10));

            //Assert
            _registeredEmployees.Count.Should().Be(2);
            _registeredEmployees[0].FullName.Should().Be("Cale Makar");
            _contractService.EmployeeContracts.Count.Should().Be(2);
            _contractService.EmployeeContracts.Any(c => c.ContractEndDate != DateTime.MinValue).Should().BeFalse();
        }

        [Fact]
        public void Register_InputInvalidSalary_ThrowsException()
        {
            //Arrange
            var testEmployee = new Employee(8, "Cale Makar", 2);

            //Act
            Action action = () => _sut.Register(testEmployee, new DateTime(2022, 10, 10));

            //Assert
            action.Should().Throw<InvalidSalaryInfoException>()
                .WithMessage($"[Employee salary: '{testEmployee.HourlySalary}' is missing or is below legal threshold]");
        }

        [Fact]
        public void Register_InputInvalidName_ThrowsException()
        {
            //Arrange
            var testEmployee = new Employee(8, " Cale", 12);

            //Act
            Action action = () => _sut.Register(testEmployee, new DateTime(2022, 10, 10));

            //Assert
            action.Should().Throw<InvalidEmployeeIdException>()
                .WithMessage($"[Invalid or missing employee information. " +
                $"Check info: '{string.Join(", ", testEmployee.FullName, testEmployee.Id)}' to solve this problem]");
        }

        [Fact]
        public void Register_InputInvalidId_ThrowsException()
        {
            //Arrange
            var testEmployee = new Employee(-2, "Cale Makar", 12);

            //Act
            Action action = () => _sut.Register(testEmployee, new DateTime(2022, 10, 10));

            //Assert
            action.Should().Throw<InvalidEmployeeIdException>()
                .WithMessage($"[Invalid or missing employee information. " +
                $"Check info: '{string.Join(", ", testEmployee.FullName, testEmployee.Id)}' to solve this problem]");
            _registeredEmployees.Any(emp => emp.FullName == testEmployee.FullName).Should().BeFalse();
        }

        [Fact]
        public void Register_InputInvalidContractInfo_ThrowsException()
        {
            //Arrange
            var testEmployee = new Employee(8, "Cale Makar", 12);

            //Act
            Action action = () => _sut.Register(testEmployee, DateTime.MinValue);

            //Assert
            action.Should().Throw<InvalidContractException>()
                .WithMessage($"[Invalid contract info. Check '{DateTime.MinValue}' to solve this problem]");
            _registeredEmployees.Any(emp => emp.FullName == testEmployee.FullName).Should().BeFalse();
        }

        [Fact]
        public void Register_InputInvalidDuplicate_ThrowsException()
        {
            //Arrange
            var testId = 8;
            var testEmployeeA = new Employee(testId, "Cale Makar", 12);
            var testEmployeeB = new Employee(testId, "Cale Makar", 12);
            _registeredEmployees.Add(testEmployeeA);
            _contractService.EmployeeContracts.Add(new EmployeeContract(testEmployeeA.Id, new DateTime(2022, 10, 01)));

            //Act
            Action action = () => _sut.Register(testEmployeeB, new DateTime(2022, 10, 04));

            //Assert
            action.Should().Throw<DuplicateContractException>()
                .WithMessage($"[Contract with the properties: '{testEmployeeB.Id}' is already registered in the system]");
            _registeredEmployees.Where(emp => emp.Id == testId).Count().Should().Be(1);
        }

        [Fact]
        public void Unregister_InputValid_CorrectEmployeeUnregistered()
        {
            //Arrange
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(16, "Mitchell Marner", 10);
            var contractEnd = new DateTime(2022, 10, 10);
            _registeredEmployees.Add(testEmployeeA);
            _registeredEmployees.Add(testEmployeeB);
            _contractService.EmployeeContracts.Add(new EmployeeContract(8, new DateTime(2021, 10, 10)));
            _contractService.EmployeeContracts.Add(new EmployeeContract(16, new DateTime(2021, 10, 10)));

            //Act
            _sut.Unregister(testEmployeeA.Id, contractEnd);

            //Assert
            _registeredEmployees.Count.Should().Be(1);
            _registeredEmployees.Any(e => e.Id == testEmployeeA.Id).Should().BeFalse();
            _contractService.EmployeeContracts
                .Any(c => c.EmployeeId == testEmployeeA.Id && c.ContractEndDate == DateTime.MinValue).Should().BeFalse();
        }

        [Fact]
        public void Unregister_InputInvalid_ThrowsException()
        {
            //Arrange
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(16, "Mitchell Marner", 10);
            _registeredEmployees.Add(testEmployeeA);

            //Act
            Action action = () => _sut.Unregister(testEmployeeB.Id, DateTime.MinValue);

            //Assert
            action.Should().Throw<EmployeeNotFoundException>()
                .WithMessage($"[Employee with the Id: '{testEmployeeB.Id}' is not found]");
        }


        [Fact]
        public void IsFound_InputValid_ReturnsIsFoundTrue()
        {
            //Arrange
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(16, "Mitchell Marner", 10);
            var testEmployeeC = new Employee(97, "Connor McDavid", 10);
            _registeredEmployees.Add(testEmployeeA);
            _registeredEmployees.Add(testEmployeeB);
            _registeredEmployees.Add(testEmployeeC);

            //Act
            var actual = _sut.IsFound(testEmployeeA.Id);

            //Assert
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsFound_InputInvalid_ThrowsException()
        {
            //Arrange
            var testId = 101;
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(16, "Mitchell Marner", 10);
            var testEmployeeC = new Employee(97, "Connor McDavid", 10);
            _registeredEmployees.Add(testEmployeeA);
            _registeredEmployees.Add(testEmployeeB);
            _registeredEmployees.Add(testEmployeeC);

            //Act
            Action action = () => _sut.IsFound(testId);

            //Assert
            action.Should().Throw<EmployeeNotFoundException>()
                .WithMessage($"[Employee with the Id: '{testId}' is not found]");
        }

        [Fact]
        public void GetById_InputValid_ReturnsCorrectEmployee()
        {
            //Arrange
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(16, "Mitchell Marner", 10);
            var testEmployeeC = new Employee(97, "Connor McDavid", 10);
            _registeredEmployees.Add(testEmployeeA);
            _registeredEmployees.Add(testEmployeeB);
            _registeredEmployees.Add(testEmployeeC);

            //Act
            var actual = _sut.GetById(testEmployeeA.Id);

            //Assert
            actual.Should().BeSameAs(testEmployeeA);
        }

        [Fact]
        public void GetById_InputInvalid_ThrowsException()
        {
            //Arrange
            var testId = 101;
            var testEmployeeA = new Employee(8, "Cale Makar", 12);
            var testEmployeeB = new Employee(16, "Mitchell Marner", 10);
            var testEmployeeC = new Employee(97, "Connor McDavid", 10);
            _registeredEmployees.Add(testEmployeeA);
            _registeredEmployees.Add(testEmployeeB);
            _registeredEmployees.Add(testEmployeeC);

            //Act
            Action action = () => _sut.GetById(testId);

            //Assert
            action.Should().Throw<EmployeeNotFoundException>()
                .WithMessage($"[Employee with the Id: '{testId}' is not found]");
        }
    }
}