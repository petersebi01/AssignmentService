using System;
using AssignmentService.Models;

namespace AssignmentService.Dto
{
    public class WorkDto
    {
        public int WorkId { get; set; }

        public int EmployeeId { get; set; }
        
        public int AssignmentId { get; set; }
        
        public DateTime StartDate { get; set; }

        public Employee Employee { get; set; }

        public Assignment Assignment { get; set; }
    }
}