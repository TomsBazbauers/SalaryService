using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Salary_Service
{
    public interface IReportValidator
    {
        public decimal MaxHours { get; set; }

        public bool IsValid(int hours, int minutes);

        public bool HasValidId(int id);

        public bool HasValidDate(DateTime date);
    }
}
