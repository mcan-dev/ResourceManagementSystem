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
                // Düz metin yerine JSON (anonim obje) dönüyoruz
                return BadRequest(new { message = "Email could not be updated." });

            // Düz metin yerine JSON (anonim obje) dönüyoruz
            return Ok(new { message = "Email updated successfully." });
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            try
            {
                // Servis katmanındaki metodunu çağırıyoruz
                await _settingsService.ChangePasswordAsync(dto);

                // İşlem başarılıysa 200 OK dönüyoruz
                return Ok(new { message = "Şifre başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                // Eğer şifre yanlışsa servisten fırlatılan hatayı ("Current password is incorrect") yakalıyoruz
                // Ve Angular'ın anlayabilmesi için 400 Bad Request olarak geri gönderiyoruz
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}