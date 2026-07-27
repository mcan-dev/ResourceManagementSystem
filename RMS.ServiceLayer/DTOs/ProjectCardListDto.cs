using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class ProjectCardListDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? ProjectDescription { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateOnly? StartDate { get; set; } 
        public DateOnly? EndDate { get; set; }
        public int? PriorityId { get; set; }
        public string PriorityName { get; set; } = string.Empty;
        public int TaskCount { get; set; }
        public int MemberCount { get; set; }
        public int TotalManDays { get; set; }
        public int CompletedTaskCount { get; set; }
        public double ProgressPercentage { get; set; }
    }
}
