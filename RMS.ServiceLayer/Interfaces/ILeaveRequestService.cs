using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IReadOnlyList<LeaveRequestAdminDto>> GetAllForAdminAsync(
            CancellationToken cancellationToken = default);

        Task UpdateStatusAsync(
            int leaveRequestId,
            int statusId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<MyLeaveRequestDto>> GetMyRequestsAsync(
        int employeeId,
        CancellationToken cancellationToken = default);

        Task CreateAsync(
            int employeeId,
            CreateLeaveRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
