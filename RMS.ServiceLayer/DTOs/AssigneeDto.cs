using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class AssigneeDto
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }
        public decimal AssignedHours { get; set; }
        public decimal CompletedHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
