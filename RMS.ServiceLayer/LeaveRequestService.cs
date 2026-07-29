using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DataLayer.Entities;
using RMS.RepositoryLayer.Interfaces;
using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer
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

        public async Task<IReadOnlyList<MyLeaveRequestDto>> GetMyRequestsAsync(
            int employeeId,
            CancellationToken cancellationToken = default)
        {
            var leaveRequests = await _leaveRequestRepository
                .GetLeaveRequestsByEmployeeIdAsync(employeeId);

            return leaveRequests
                .Select(l => new MyLeaveRequestDto
                {
                    Id = l.Id,
                    LeaveTypeName = l.LeaveType?.Name ?? string.Empty,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    TotalDays = l.TotalDays ?? 0,
                    StatusId = l.LeaveStatusId ?? 0,
                    StatusName = l.LeaveStatus?.LeaveStatusName ?? string.Empty
                })
                .ToList();
        }

        public async Task CreateAsync(
            int employeeId,
            CreateLeaveRequestDto request,
            CancellationToken cancellationToken = default)
        {

            // Başlangıç tarihi bitiş tarihinden sonra olamaz.
            if (request.StartDate > request.EndDate)
            {
                throw new ArgumentException("Start date cannot be later than end date.");
            }

            // Geçmiş tarih için izin oluşturulamaz.
            if (request.StartDate < DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ArgumentException("Leave request cannot start in the past.");
            }

            var leaveRequest = new LeaveRequest
            {
                EmployeeId = employeeId,
                LeaveTypeId = request.LeaveTypeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalDays = request.EndDate.DayNumber - request.StartDate.DayNumber + 1,
                LeaveStatusId = 1, // Beklemede
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _leaveRequestRepository.AddAsync(leaveRequest);
            await _leaveRequestRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
