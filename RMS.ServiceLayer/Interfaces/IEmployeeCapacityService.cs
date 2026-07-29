using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.ServiceLayer.DTOs;

namespace RMS.ServiceLayer.Interfaces
{
    public interface IEmployeeCapacityService
    {
        Task<IReadOnlyList<TeamCapacityDto>> GetTeamCapacitiesAsync();

        Task<IReadOnlyList<EmployeeWorkloadDto>> GetEmployeeWorkloadsAsync();

        Task<IReadOnlyList<EmployeeCapacityDetailDto>> GetEmployeeCapacityDetailsAsync(int employeeId);

        Task<IReadOnlyList<EmployeeCapacityDto>> GetCapacitiesAsync(int? employeeId = null);

        Task<EmployeeCapacityDto> CreateCapacityAsync(CreateEmployeeCapacityRequest request);

        Task<EmployeeCapacityDto?> UpdateCapacityAsync(int id, UpdateEmployeeCapacityRequest request);

        Task<EmployeeCapacitySummaryDto?> GetEmployeeCapacitySummaryAsync(int employeeId);


    }
}
