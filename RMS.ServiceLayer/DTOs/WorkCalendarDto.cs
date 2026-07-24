using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class WorkCalendarDto
    {
        public DateOnly CalendarDate { get; set; }

        public bool IsWorkingDay { get; set; }

        public decimal WorkingHours { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;
    }
}
