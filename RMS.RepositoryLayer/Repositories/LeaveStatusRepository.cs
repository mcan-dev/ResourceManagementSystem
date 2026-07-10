using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;
using RMS.RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Repositories
{
    public class LeaveStatusRepository : GenericRepository<LeaveStatus>, ILeaveStatusRepository
    {
        public LeaveStatusRepository(RmsContext context) : base(context)
        {
          
        }
    }
}
