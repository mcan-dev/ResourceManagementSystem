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
    public class TaskProgressRepository : GenericRepository<TaskProgress>, ITaskProgressRepository
    {
        public TaskProgressRepository(RmsContext context) : base(context)
        {

        }
    }
}
