using Employee_Salary_Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Salary_Service
{
    public class ReportValidator : IReportValidator
    {
        public decimal maxHours = 8;

        public decimal MaxHours { get; set; }

        public bool IsValid(int hours, int minutes)
        {
            var total = hours + minutes / 60;
            return total < maxHours && total > 0 ? true : throw new InvalidHoursException(string.Join("", total));
        }

        public bool HasValidId(int id)
        {
            var info = id.ToString();
            return id > 0 ? true : throw new InvalidEmployeeIdException(info);
        }

        public bool HasValidDate(DateTime date)
        {
            return date != DateTime.MinValue && date <= DateTime.Now ? true : throw new InvalidReportDateException(date.ToString());
        }
    }
}