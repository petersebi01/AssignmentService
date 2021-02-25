using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentService.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Firstname { get; set; }
        
        [Required]
        public string Lastname { get; set; }
        
        public int Payment { get; set; }
        
        public IList<Work> Works { get; set; }
    }
}