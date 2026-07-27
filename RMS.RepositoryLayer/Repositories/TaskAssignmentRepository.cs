using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Repositories
{
    public class TaskAssignmentRepository : GenericRepository<TaskAssignment>, ITaskAssignmentRepository
    {
        public TaskAssignmentRepository (RmsContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TaskAssignment>> GetAllWithDetailsAsync(Expression<Func<TaskAssignment, bool>> filter = null)
        {
            IQueryable<TaskAssignment> query = _context.Set<TaskAssignment>()
                .Include(ta => ta.Task)
                .ThenInclude(pt => pt.Project)
                .Include(ta => ta.TaskProgress) 
                .Include(ta => ta.Employee);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    }
}

