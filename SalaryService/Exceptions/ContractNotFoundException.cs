namespace SalaryService
{
    public class ContractNotFoundException : Exception
    {
        public ContractNotFoundException(string id) : base($"[Contract with the employee Id: '{id}' is not found]")
        { }
    }
}