using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer;
using RMS.ServiceLayer.DTOs;

namespace RMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPut("email")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDto dto)
        {
            var result = await _settingsService.ChangeEmailAsync(dto);

            if (!result)
                
                return BadRequest(new { message = "Email could not be updated." });

          
            return Ok(new { message = "Email updated successfully." });
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                await _settingsService.ChangePasswordAsync(dto);

                return Ok(new { message = "Şifre başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}