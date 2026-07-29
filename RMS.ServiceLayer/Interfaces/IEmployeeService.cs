using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.ServiceLayer.DTOs;
namespace RMS.ServiceLayer.DTOs;

public interface IEmployeeService
{
    Task<IReadOnlyList<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<EmployeeDto?> GetEmployeeByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmployeeDto>> GetEmployeesByTeamAsync(int teamId, CancellationToken cancellationToken = default);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken = default);
    Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeListDto>> GetAllEmployeesForDropdownAsync();
}
