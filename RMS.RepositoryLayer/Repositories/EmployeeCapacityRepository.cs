using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RMS.DataLayer.Entities;
using RMS.DataLayer.RmsDb;
using RMS.RepositoryLayer.Interfaces;

namespace RMS.RepositoryLayer.Repositories
{
    public class EmployeeCapacityRepository : GenericRepository<EmployeeCapacity>, IEmployeeCapacityRepository
    {
        public EmployeeCapacityRepository(RmsContext context) : base(context)
        {

        }
        public async Task<bool> ExistsAsync(int employeeId)
        {
            return await _context.EmployeeCapacities.AnyAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<IReadOnlyList<EmployeeCapacity>> GetEmployeeCapacitiesByTeamAsync(int teamId)
        {
            return await _context.EmployeeCapacities
                .Include(ec => ec.Employee)
                    .ThenInclude(e => e.Team)
                .Include(ec => ec.Employee)
                    .ThenInclude(e => e.Title)
                .Where(ec => ec.Employee.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<EmployeeCapacity>> GetEmployeeCapacitiesWithDetailsAsync()
        {
            return await _context.EmployeeCapacities
                .Include(ec => ec.Employee)
                    .ThenInclude(e => e.Team)
                .Include(ec => ec.Employee)
                    .ThenInclude(e => e.Title)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<EmployeeCapacity>> GetEmployeeCapacitiesByEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeCapacities
                .Include(ec => ec.Employee)
                    .ThenInclude(e => e.Team)
                .Include(ec => ec.Employee)
                    .ThenInclude(e => e.Title)
                .Where(ec => ec.EmployeeId == employeeId)
                .ToListAsync();
        }
    }
}


