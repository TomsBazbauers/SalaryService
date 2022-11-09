namespace SalaryService
{
    public class EmployeeContract
    {
        public EmployeeContract(int id, DateTime contractStart, DateTime? contractEnd = null)
        {
            EmployeeId = id;
            ContractStartDate = contractStart;
            ContractEndDate = contractEnd ?? DateTime.MinValue;
        }

        public int EmployeeId { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }
    }
}