using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentService.Models;

namespace AssignmentService.Dto
{
    public class AssignmentDto
    {
        public int AssignmentId { get; set; }

        public string AssignmentName { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime FinishDate { get; set; }
        
        // public IList<Employee> Employees { get; set; }

        public IList<Work> Works { get; set; }
        
        public int TaskId { get; set; }

    }
}