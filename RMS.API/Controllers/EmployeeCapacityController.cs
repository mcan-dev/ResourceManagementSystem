using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeCapacityController : ControllerBase
{
    private readonly IEmployeeCapacityService _employeeCapacityService;

    public EmployeeCapacityController(IEmployeeCapacityService employeeCapacityService)
    {
        _employeeCapacityService = employeeCapacityService;
    }

    [HttpGet("team-summary")]
    public async Task<IActionResult> GetTeamSummary()
    {
        var result = await _employeeCapacityService.GetTeamCapacitiesAsync();
        return Ok(result);
    }

    [HttpGet("workloads")]
    public async Task<IActionResult> GetEmployeeWorkloads()
    {
        var result = await _employeeCapacityService.GetEmployeeWorkloadsAsync();
        return Ok(result);
    }
    [HttpGet("{employeeId}/details")]
    public async Task<IActionResult> GetEmployeeCapacityDetails(int employeeId)
    {
        var result = await _employeeCapacityService.GetEmployeeCapacityDetailsAsync(employeeId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetCapacities(
    [FromQuery] int? employeeId)
    {
        var capacities = await _employeeCapacityService
            .GetCapacitiesAsync(employeeId);

        return Ok(capacities);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCapacity(
    [FromBody] CreateEmployeeCapacityRequest request)
    {
        try
        {
            var capacity = await _employeeCapacityService.CreateCapacityAsync(request);

            return CreatedAtAction(
                nameof(GetCapacities),
                new { id = capacity.Id },
                capacity);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCapacity(
    int id,
    [FromBody] UpdateEmployeeCapacityRequest request)
    {
        try
        {
            var capacity = await _employeeCapacityService.UpdateCapacityAsync(id, request);

            if (capacity == null)
            {
                return NotFound();
            }

            return Ok(capacity);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }
    [HttpGet("{id}/capacity-summary")]
    public async Task<IActionResult> GetEmployeeCapacitySummary(int id)
    {
        var summary = await _employeeCapacityService.GetEmployeeCapacitySummaryAsync(id);

        if (summary == null)
        {
            return NotFound();
        }

        return Ok(summary);
    }
}