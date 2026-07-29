using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using RMS.ServiceLayer.Services;

namespace RMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly ITaskAssignmentService _taskService;

        public TaskAssignmentsController(ITaskAssignmentService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetEmployeeTasks(int employeeId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByEmployeeIdAsync(employeeId);

                if (tasks == null || tasks.Count == 0)
                {
                    return Ok(new List<object>()); 
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Personel görevleri getirilirken bir hata oluştu.", Details = ex.Message });
            }
        }

        [HttpGet("employee/manager")]
        public async Task<IActionResult> GetAllManagerTasks()
        {
            try
            {
                
                var managerTasks = await _taskService.GetAllManagerTasksAsync();

                return Ok(managerTasks);
            }
            catch (Exception ex)
            {              
                return StatusCode(500, $"Yönetici görevleri getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost("create-with-assignments")]
        public async Task<IActionResult> CreateTaskWithAssignments([FromBody] CreateTaskWithAssignmentsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isSuccess = await _taskService.CreateTaskWithAssignmentsAsync(dto);

            if (isSuccess)
                return Ok(new { Message = "Görev ve atamalar başarıyla kaydedildi." });

            return BadRequest(new { Message = "İşlem sırasında bir hata oluştu." });
        }


        [HttpPut("update-progress")]
        public async Task<IActionResult> UpdateEmployeeProgress([FromBody] UpdateEmployeeProgressDto dto)
        {
           
            if (dto.CompletedHours < 0)
                return BadRequest("Tamamlanan saat 0'dan küçük olamaz.");

            var success = await _taskService.UpdateCompletedHoursAsync(dto);

            if (!success)
                return NotFound("İlgili görev veya personel ataması bulunamadı.");

            return Ok(new { message = "İlerleme başarıyla güncellendi." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
           
            var isDeleted = await _taskService.DeleteTaskAsync(id);

            if (!isDeleted)
            {
              
                return NotFound(new { Message = "Silinmek istenen görev bulunamadı." });
            }

            return NoContent();
        }

        [HttpPost("add-assignment")]
        public async Task<IActionResult> AddSingleAssignment([FromBody] AddAssignmentDto dto)
        {
            if (dto == null || dto.AssignedHours <= 0)
                return BadRequest("Geçersiz atama verisi.");

            await _taskService.AddSingleAssignmentAsync(dto);
            return Ok(new { message = "Personel ataması başarıyla eklendi." });
        }

        
        [HttpPut("update-hours/{id}")]
        public async Task<IActionResult> UpdateHours(int id, [FromBody] UpdateAssignmentHoursDto dto)
        {
            if (dto.AssignedHours <= 0)
                return BadRequest("Saat değeri 0'dan büyük olmalıdır.");
            // buraya capacityi aşar bloğuda eklicek eğer atanan assigned hours halihazırda emplooyenin kapasitesini aşıyorsa o saati veremezsin işte toplam bu kadar saati var bunu verbilirsin tarzı
            await _taskService.UpdateAssignmentHoursAsync(id, dto.AssignedHours);
            return Ok(new { message = "Saat başarıyla güncellendi." });
        }
  
        [HttpDelete("assignment/{id}")]
        public async Task<IActionResult> DeleteSingleAssignment(int id)
        {
            await _taskService.DeleteSingleAssignmentAsync(id);
            return NoContent(); 
        }
    }
}
    


