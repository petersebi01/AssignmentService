using System;
using System.Collections.Generic;
using System.Linq;
using AssignmentService.Data.Contexts;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public class SqlTaskRepository : ITaskRepository
    {
        private readonly AssignmentServiceDbContext _dbContext;
        
        public SqlTaskRepository(AssignmentServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _dbContext.Tasks.ToList();
        }

        public Task GetTaskById(int id)
        {
            return _dbContext.Tasks.FirstOrDefault(p => p.TaskId == id);
        }

        public IQueryable<Employee> GetEmployeesOnTask(int id)
        {
            return _dbContext.Tasks.Where(t => t.TaskId == id)
                .Join(_dbContext.Assignments, t => t.TaskId, a => a.TaskId, (t, a) => a)
                .Join(_dbContext.Works, a => a.AssignmentId, w => w.AssignmentId, (a, w) => w)
                .Join(_dbContext.Employees, w => w.EmployeeId, e => e.EmployeeId, (ta, e) => e);
        }

        public IQueryable<Assignment> GetAssignmentsOnTask(int id)
        {
            return _dbContext.Tasks.Where(t => t.TaskId == id).Join(_dbContext.Assignments, t => t.TaskId, a => a.TaskId, (t, a) => a);
        }

        public void CreateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _dbContext.Tasks.Add(task);
        }

        public void UpdateTask(Task task)
        {
            //
        }

        public void DeleteTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Tasks.Remove(task);
        }
    }
}