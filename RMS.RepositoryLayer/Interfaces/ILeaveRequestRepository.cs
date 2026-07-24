using RMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Interfaces
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByStatusAsync(int statusId);
        Task<IEnumerable<LeaveRequest>> GetMonthlyLeaveRequestsAsync(
            int year,
            int month,
            CancellationToken cancellationToken = default);
    }
}
