using RMS.DataLayer.Entities;
using RMS.ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Interfaces
{
    public interface ITaskAssignmentService
    {
        Task<List<EmployeeTaskListDto>> GetTasksByEmployeeIdAsync(int employeeId);

        Task<List<ManagerTaskListDto>> GetAllManagerTasksAsync();
        Task<bool> CreateTaskWithAssignmentsAsync(CreateTaskWithAssignmentsDto dto);
        Task<bool> DeleteTaskAsync(int taskId);
        Task AddSingleAssignmentAsync(AddAssignmentDto dto);
        Task UpdateAssignmentHoursAsync(int id, int newHours);
        Task<bool> UpdateCompletedHoursAsync(UpdateEmployeeProgressDto dto);
        Task<bool> DeleteSingleAssignmentAsync(int id);
    }
}
