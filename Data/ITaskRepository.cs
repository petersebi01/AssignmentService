using System.Collections.Generic;
using System.Linq;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public interface ITaskRepository
    {
        bool SaveChanges();
        IEnumerable<Task> GetAllTasks();
        Task GetTaskById(int id);

        IQueryable<Employee> GetEmployeesOnTask(int id);

        IQueryable<Assignment> GetAssignmentsOnTask(int id);
        // Task GetTaskOnAssignment(int id);
        void CreateTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(Task task);
    }
}