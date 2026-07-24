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
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(RmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(int employeeId)
        {
            return await _context.LeaveRequests
                .Where(lr => lr.EmployeeId == employeeId)
                .Include(lr => lr.LeaveStatus) 
                .ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByStatusAsync(int statusId)
        {
            return await _context.LeaveRequests
                .Where(lr => lr.LeaveStatusId == statusId)
                .Include(lr => lr.Employee) 
                .ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetMonthlyLeaveRequestsAsync(
    int year,
    int month,
    CancellationToken cancellationToken = default)
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(l => l.LeaveStatus)
                .Where(l =>
                    (l.StartDate.Year == year && l.StartDate.Month == month) ||
                    (l.EndDate.Year == year && l.EndDate.Month == month))
                .ToListAsync(cancellationToken);
        }
    }
}
