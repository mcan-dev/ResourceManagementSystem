using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Repositories
{
    public interface IWorkCalendarRepository : IGenericRepository<WorkCalendar>
    {
        Task<IReadOnlyList<WorkCalendar>> GetMonthlyCalendarAsync(
    int year,
    int month,
    CancellationToken cancellationToken = default);
    }
}
