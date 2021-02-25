using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentService.Models;

namespace AssignmentService.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        
        public string TaskName { get; set; }
        
        [MaxLength(100)]
        public string TaskDescription { get; set; }
        
        public DateTime ExpirationDate { get; set; }

        //public IList<Assignment> Assignments { get; set; }
    }
}