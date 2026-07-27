using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.Interfaces;

namespace RMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMonthlyCalendar(
        [FromQuery] int year,
        [FromQuery] int month,
        CancellationToken cancellationToken)
    {
        var result = await _calendarService.GetMonthlyCalendarAsync(
            year,
            month,
            cancellationToken);

        return Ok(result);
    }
}