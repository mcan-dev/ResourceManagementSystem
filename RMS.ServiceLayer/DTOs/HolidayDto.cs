using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class HolidayDto
    {
        public DateOnly Date { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
