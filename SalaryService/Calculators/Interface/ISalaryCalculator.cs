using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Salary_Service
{
    public interface ISalaryCalculator
    {
        public decimal GetSalary(List<EmployeeDailyReport> dailyReports, decimal hourlySalary);
    }
}
