using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer.Interfaces
{
    public interface ICalendarService
    {
        Task<CalendarDto> GetMonthlyCalendarAsync(
            int year,
            int month,
            CancellationToken cancellationToken = default);
    }
}
