using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentService.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        
        [Required]
        public string AssignmentName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required] public DateTime FinishDate { get; set; }

        // [Required] public IList<Employee> Employees { get; set; }

        public IList<Work> Works { get; set; }

        [Required]
        public int TaskId { get; set; }
        
    }
}