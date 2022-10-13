using Employee_Salary_Service.Exceptions;
namespace Employee_Salary_Service.Exceptions
{
    public class InvalidContractException : Exception
    {
        public InvalidContractException(string info) : base($"[Invalid contract info. Check '{info}' to solve this problem]")
        {}
    }
}