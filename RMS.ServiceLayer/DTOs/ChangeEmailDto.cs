using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class ChangeEmailDto
    {
        public int EmployeeId { get; set; }

        public string NewEmail { get; set; } = string.Empty;

        public string ConfirmEmail { get; set; } = string.Empty;
    }
}