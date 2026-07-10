using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Repositories
{
    internal class WorkCalendarRepository : GenericRepository<WorkCalendar>, IWorkCalendarRepository
    {
        public WorkCalendarRepository(RmsContext context) : base(context)
        {

        }
    }
}
