using System.Collections.Generic;
using AssignmentService.Models;

namespace AssignmentService.Dto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Firstname { get; set; }
        
        public string Lastname { get; set; }

        public int Payment { get; set; }

        // public Assignment Assignment { get; set; }

        public IList<Work> Works { get; set; }
    }
}