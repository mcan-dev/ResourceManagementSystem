using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class ProjectTaskDetailDto
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string AssigneeName { get; set; } = string.Empty;
        public decimal? AssignedHours { get; set; }
        public decimal TotalHours { get; set; }
        public decimal CompletedHours { get; set; }
        public string AssigneeTitle { get; set; } = string.Empty;
        public string PriorityName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;

        public double ProgressPercentage { get; set; }
        public DateOnly? Deadline { get; set; }
    }
}
