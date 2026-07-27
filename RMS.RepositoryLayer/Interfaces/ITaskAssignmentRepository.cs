using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Interfaces
{
    public interface ITaskAssignmentRepository : IGenericRepository<TaskAssignment>
    {
        Task<IEnumerable<TaskAssignment>> GetAllWithDetailsAsync(Expression<Func<TaskAssignment, bool>> filter = null);
    }   
}
