using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class CalendarDto
    {
        public List<WorkCalendarDto> WorkDays { get; set; } = [];

        public List<LeaveCalendarDto> Leaves { get; set; } = [];

        public List<HolidayDto> Holidays { get; set; } = [];
    }
}
