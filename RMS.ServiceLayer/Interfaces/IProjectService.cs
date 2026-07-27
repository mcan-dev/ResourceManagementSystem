using RMS.DataLayer.Entities;
using RMS.ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDetailDto?> GetProjectDetailsAsync(int projectId);
        Task<List<ProjectCardListDto>> GetProjectCardsAsync();
        Task<Project> UpdateProjectAsync(int id, ProjectUpdateDto projectDto);
        Task<Project> AddProjectAsync(ProjectCreateDto projectDto);
        Task<bool> DeleteProjectAsync(int projectId);

    }
}