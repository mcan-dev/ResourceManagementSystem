using Microsoft.AspNetCore.Mvc;
using RMS.ServiceLayer.DTOs;
using RMS.ServiceLayer.Interfaces;
using System.Threading.Tasks;

namespace RMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectCards()
        {
            var projects = await _projectService.GetProjectCardsAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectDetails(int id)
        {
            var projectDetail = await _projectService.GetProjectDetailsAsync(id);

            if (projectDetail == null)
            {
                return NotFound(new { Message = "Aradığınız proje bulunamadı." });
            }

            return Ok(projectDetail);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto projectDto)
        {
            try
            {
                var createdProject = await _projectService.AddProjectAsync(projectDto);
                return Ok(new
                {
                    Id = createdProject.Id,
                    ProjectName = createdProject.ProjectName,
                    ProjectDescription = createdProject.ProjectDescription,
                    StartDate = createdProject.StartDate,
                    EndDate = createdProject.EndDate,
                    ProjectStatusId = createdProject.ProjectStatusId,
                    PriorityId = createdProject.PriorityId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Proje oluşturulurken sistemsel bir hata meydana geldi.", Details = ex.Message });
            }

        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectUpdateDto projectDto)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(id, projectDto);
               return Ok(new
                {
                    ProjectName = updatedProject.ProjectName,
                    StartDate = updatedProject.StartDate,
                    EndDate = updatedProject.EndDate,
                    PriorityId = updatedProject.PriorityId,
                    ProjectStatusId = updatedProject.ProjectStatusId,
                    ProjectDescription = updatedProject.ProjectDescription,
                    UpdatedAt = updatedProject.UpdatedAt
               });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Proje güncellenirken sistemsel bir hata meydana geldi.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {

                var isDeleted = await _projectService.DeleteProjectAsync(id);


                if (!isDeleted)
                {
                    return NotFound(new { Message = "Silinmek istenen proje bulunamadı." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Proje silinirken sistemsel bir hata meydana geldi.", Details = ex.Message });
            }
        } 
    }
}
