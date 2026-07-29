using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.Interfaces;

namespace RMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await _dashboardService.GetAdminDashboardDataAsync();
            return Ok(result);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeDashboard(int employeeId)
        {
            var result = await _dashboardService.GetEmployeeDashboardDataAsync(employeeId);

            if (result == null)
            {
                return NotFound("Personel paneli verisi bulunamadı.");
            }

            return Ok(result);
        }
    }
}