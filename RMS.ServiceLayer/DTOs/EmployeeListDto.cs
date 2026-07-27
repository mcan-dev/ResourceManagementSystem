using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ServiceLayer.DTOs
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

       
        public string FullName => $"{Name} {Surname}";
    }
}
