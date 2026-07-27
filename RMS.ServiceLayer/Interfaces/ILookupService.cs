using RMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<ProjectStatus>> GetProjectStatusesAsync();
        Task<IEnumerable<Priority>> GetPrioritiesAsync();
        Task<IEnumerable<ProjectRole>> GetProjectRolesAsync();
        Task<IEnumerable<EmployeePriority>> GetEmployeePrioritiesAsync();
    }
}
