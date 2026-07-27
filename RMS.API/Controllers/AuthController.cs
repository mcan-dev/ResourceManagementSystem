using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.DTOs;
using System.Linq;
using RMS.DataLayer.RmsDb; 

namespace RMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RmsContext _context;

        public AuthController(RmsContext context)
        {
            _context = context;
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { Message = "Sistem ayakta! Bağlantı kopmadı." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
              
                if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                    return BadRequest(new { Message = "E-posta ve şifre boş bırakılamaz." });


                var user = _context.Employees.FirstOrDefault(u =>
                    u.Email == loginDto.Email &&
                    u.PasswordHash == loginDto.Password);



                if (user == null)

                    return Unauthorized(new { Message = "Hatalı e-posta veya şifre girdiniz." });

               
                if (user.Status != "Aktif")
                    return BadRequest(new { Message = "Hesabınız aktif durumda değil." });
                }


                return Ok(new
                {
                    Message = "Giriş başarılı!",
                    UserId = user.Id,
                    UserName = $"{user.Name} {user.Surname}",
                    Role = user.UserRole
                });
            }
            catch (System.Exception ex)
            {
              
                var gercekHata = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, new
                {
                    Message = "SİSTEM ÇÖKTÜ, GİZLİ HATA BULUNDU:",
                    HataDetayi = gercekHata
                });
            }
        }
    }
}