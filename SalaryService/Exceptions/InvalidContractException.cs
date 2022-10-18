namespace SalaryService
{
    public class InvalidContractException : Exception
    {
        public InvalidContractException(string info) : base($"[Invalid contract info. Check '{info}' to solve this problem]")
        { }
    }
}