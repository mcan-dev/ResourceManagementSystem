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
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
        public PriorityRepository(RmsContext context) : base(context)
        {

        }
    }
}
