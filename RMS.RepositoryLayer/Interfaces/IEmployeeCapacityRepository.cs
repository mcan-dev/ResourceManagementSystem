using RMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Interfaces;

public interface IEmployeeCapacityRepository : IGenericRepository<EmployeeCapacity>
{
    Task<IReadOnlyList<EmployeeCapacity>> GetEmployeeCapacitiesWithDetailsAsync();

    Task<IReadOnlyList<EmployeeCapacity>> GetEmployeeCapacitiesByTeamAsync(int teamId);

    Task<IReadOnlyList<EmployeeCapacity>> GetEmployeeCapacitiesByEmployeeAsync(int employeeId);
    Task<bool> ExistsAsync(int employeeId, int projectId);
}