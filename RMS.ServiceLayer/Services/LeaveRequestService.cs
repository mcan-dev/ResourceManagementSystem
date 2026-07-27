using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;

namespace RMS.ServiceLayer.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<IReadOnlyList<LeaveRequestAdminDto>> GetAllForAdminAsync(
             CancellationToken cancellationToken = default)
        {
            var leaveRequests = await _leaveRequestRepository
                .GetAllForAdminAsync(cancellationToken);

            return leaveRequests.Select(l => new LeaveRequestAdminDto
            {
                Id = l.Id,

                EmployeeId = l.EmployeeId,

                EmployeeName = $"{l.Employee.Name} {l.Employee.Surname}",

                TeamName = l.Employee.Team?.TeamName ?? string.Empty,

                LeaveTypeId = l.LeaveTypeId ?? 0,

                LeaveTypeName = l.LeaveType?.Name ?? string.Empty,

                IsPaid = l.LeaveType?.IsPaid ?? false,

                StartDate = l.StartDate,

                EndDate = l.EndDate,

                TotalDays = l.TotalDays ?? 0,

                StatusId = l.LeaveStatusId ?? 0,

                StatusName = l.LeaveStatus?.LeaveStatusName ?? string.Empty
            }).ToList();
        }
        public async Task UpdateStatusAsync(
            int leaveRequestId,
            int statusId,
            CancellationToken cancellationToken = default)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(leaveRequestId);

            if (leaveRequest is null)
            {
                throw new KeyNotFoundException("Leave request not found.");
            }

            leaveRequest.LeaveStatusId = statusId;
            leaveRequest.UpdatedAt = DateTime.UtcNow;

            _leaveRequestRepository.Update(leaveRequest);

            await _leaveRequestRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
