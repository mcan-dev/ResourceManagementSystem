using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Repositories
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
    }
}
