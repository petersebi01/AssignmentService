using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentService.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        
        [Required]
        public string TaskName { get; set; }
        
        [MaxLength(100)]
        public string TaskDescription { get; set; }

        public DateTime ExpirationDate { get; set; }

        //public IList<Assignment> Assignments { get; set; }
    }
}