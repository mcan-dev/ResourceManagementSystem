using Microsoft.EntityFrameworkCore;
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
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(RmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Where(pt => pt.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetAllWithDetailsAsync()
        {
            return await _context.ProjectTasks
                .Include(pt => pt.Project) // Proje adının gelmesi için
                .Include(pt => pt.TaskAssignments)
                    .ThenInclude(ta => ta.Employee) // Personel adlarının gelmesi için
                .Include(pt => pt.TaskAssignments)
                    .ThenInclude(ta => ta.TaskProgress) // Tamamlanan saatlerin gelmesi için
                .ToListAsync();
        }
    }
}
