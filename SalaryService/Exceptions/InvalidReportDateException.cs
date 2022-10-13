using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Salary_Service.Exceptions
{
    public class InvalidReportDateException : Exception
    {
        public InvalidReportDateException(string info) : base($"[Invalid date info. Check '{info}' to solve this problem]")
        {}
    }
}
