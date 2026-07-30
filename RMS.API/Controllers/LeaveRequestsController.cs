using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestsController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    [HttpGet("admin-list")]
    public async Task<IActionResult> GetAllForAdmin(CancellationToken cancellationToken)
    {
        var result = await _leaveRequestService.GetAllForAdminAsync(cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
    int id,
    [FromBody] UpdateLeaveRequestStatusDto request,
    CancellationToken cancellationToken)
    {
        try
        {
            await _leaveRequestService.UpdateStatusAsync(
                id,
                request.StatusId,
                cancellationToken);

            return Ok(new
            {
                Message = "Leave request status updated successfully."
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                Message = ex.Message
            });
        }
    }

    [HttpGet("my-requests")]
    public async Task<IActionResult> GetMyRequests(
 CancellationToken cancellationToken)
    {
        // Geçici olarak sabit kullanıcı
        int employeeId = 1;

        var result = await _leaveRequestService.GetMyRequestsAsync(
            employeeId,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateLeaveRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            int employeeId = 1;

            await _leaveRequestService.CreateAsync(
                employeeId,
                request,
                cancellationToken);

            return Ok(new
            {
                Message = "Leave request created successfully."
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }
}