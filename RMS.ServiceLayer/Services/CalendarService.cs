using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.RepositoryLayer.Interfaces;
using RMS.RepositoryLayer.Repositories;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;

namespace RMS.ServiceLayer.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IWorkCalendarRepository _workCalendarRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;


        public CalendarService(
     IWorkCalendarRepository workCalendarRepository,
     ILeaveRequestRepository leaveRequestRepository)
        {
            _workCalendarRepository = workCalendarRepository;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<CalendarDto> GetMonthlyCalendarAsync(
    int year,
    int month,
    CancellationToken cancellationToken = default)
        {
            var workCalendars = await _workCalendarRepository
                .GetMonthlyCalendarAsync(year, month, cancellationToken);

            var leaves = await _leaveRequestRepository
    .GetMonthlyLeaveRequestsAsync(year, month, cancellationToken);

            var calendar = new CalendarDto
            {
                WorkDays = workCalendars.Select(w => new WorkCalendarDto
                {
                    CalendarDate = w.CalendarDate,
                    IsWorkingDay = w.IsWorkingDay,
                    WorkingHours = w.WorkingHours ?? 0,
                    EmployeeId = w.EmployeeId ?? 0,
                    EmployeeName = w.Employee?.Name ?? string.Empty
                }).ToList(),

                Leaves = leaves.Select(l => new LeaveCalendarDto

                {
                    EmployeeId = l.EmployeeId,
                    EmployeeName = l.Employee?.Name ?? string.Empty,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    LeaveType = l.LeaveType?.Name
                }).ToList(),

                Holidays = new List<HolidayDto>
                {
                    new HolidayDto
                    {
                        Date = new DateOnly(2026, 7, 15),
                        Name = "Demokrasi ve Millî Birlik Günü"
                    },
                    new HolidayDto
                    {
                        Date = new DateOnly(2026, 8, 30),
                        Name = "Zafer Bayramı"
                    }
                }
                            };

            return calendar;
        }
    }
}
