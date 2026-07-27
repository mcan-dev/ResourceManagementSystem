using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;

namespace RMS.RepositoryLayer.Repositories
{
    public class WorkCalendarRepository : GenericRepository<WorkCalendar>, IWorkCalendarRepository
    {
        public WorkCalendarRepository(RmsContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<WorkCalendar>> GetMonthlyCalendarAsync(
    int year,
    int month,
    CancellationToken cancellationToken = default)
        {
            return await _context.WorkCalendars
                .Include(w => w.Employee)
                .Where(w => w.CalendarDate.Year == year &&
                            w.CalendarDate.Month == month)
                .OrderBy(w => w.CalendarDate)
                .ToListAsync(cancellationToken);
        }
    }
}
