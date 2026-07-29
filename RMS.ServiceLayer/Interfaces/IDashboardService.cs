using RMS.ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Interfaces
{
    public interface IDashboardService
    {
        Task<AdminDashboardDto> GetAdminDashboardDataAsync();
        Task<EmployeeDashboardDto> GetEmployeeDashboardDataAsync(int employeeId);
    }
}
