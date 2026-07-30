using RMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.RepositoryLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllEmployeesWithDetailsAsync();
        Task<Employee?> GetEmployeeWithDetailsByIdAsync(int id);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetEmployeesByTeamIdAsync(int teamId);
        Task<bool> UpdateEmailAsync(int employeeId, string newEmail);
        Task<bool> UpdatePasswordAsync(int employeeId, string newPassword);
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    }
}
