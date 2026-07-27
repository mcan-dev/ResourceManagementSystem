namespace RMS.ServiceLayer.DTOs
{
    public class ProjectCreateDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public int? PriorityId { get; set; }
        public string? ProjectDescription { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public int ProjectStatusId { get; set; }
    }
}