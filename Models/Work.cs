using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentService.Models
{
    public class Work
    {
        [Key] 
        public int WorkId { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }
        
        [Required]
        public int AssignmentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public Employee Employee { get; set; }

        public Assignment Assignment { get; set; }
    }
}