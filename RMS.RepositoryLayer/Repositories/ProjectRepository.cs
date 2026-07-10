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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(RmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectStatus)
                .Include(p => p.Priority)     
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(int statusId)
        {
            return await _context.Projects
                .Where(p => p.ProjectStatusId == statusId)
                .Include(p => p.Priority)
                .ToListAsync();
        }
    }
}
