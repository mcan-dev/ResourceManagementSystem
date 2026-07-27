using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class EmployeeTaskListDto
    {
       
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? ProjectName { get; set; }
        public string? TaskStatus { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public decimal AssignedHours { get; set; }
        public decimal CompletedHours { get; set; }
        public decimal RemainingHours => AssignedHours - CompletedHours;
        public int ProgressPercentage => AssignedHours > 0
            ? (int)Math.Round((decimal)CompletedHours / AssignedHours * 100, 0)
            : 0;
    }


}
