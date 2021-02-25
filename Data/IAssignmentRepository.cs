using System.Collections.Generic;
using System.Linq;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public interface IAssignmentRepository
    {
        bool SaveChanges();
        IEnumerable<Assignment> GetAllAssignments();
        Assignment GetAssignmentById(int id);
        IEnumerable<Employee> GetEmployeesOnAssignment(int id);

        Task GetTaskOnAssignment(int id);
        void CreateAssignment(Assignment assignment);
        void UpdateAssignment(Assignment assignment);
        void DeleteAssignment(Assignment assignment);
    }
}