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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesWithDetailsAsync()
        {
            return await _context.Employees
                .Include(e => e.TeamId)
                .Include(e => e.TitleId)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeWithDetailsByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.TeamId)
                .Include(e => e.TitleId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByTeamIdAsync(int teamId)
        {
            return await _context.Employees
                .Where(e => e.TeamId == teamId)
                .ToListAsync();
        }
    }
}