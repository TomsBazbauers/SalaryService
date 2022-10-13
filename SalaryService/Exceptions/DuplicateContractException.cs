namespace Employee_Salary_Service.Exceptions
{
    public class DuplicateContractException : Exception
    {
        public DuplicateContractException(string info) : base($"[Contract with the properties: '{info}' is already registered in the system]")
        { }
    }
}
