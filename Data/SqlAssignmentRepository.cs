using System;
using System.Collections.Generic;
using System.Linq;
using AssignmentService.Data.Contexts;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public class SqlAssignmentRepository : IAssignmentRepository
    {
        private readonly AssignmentServiceDbContext _dbContext;

        public SqlAssignmentRepository(AssignmentServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public IEnumerable<Assignment> GetAllAssignments()
        {
            return _dbContext.Assignments.ToList();
        }

        public Assignment GetAssignmentById(int id)
        {
            return _dbContext.Assignments.FirstOrDefault(p => p.AssignmentId == id);
        }

        public IEnumerable<Employee> GetEmployeesOnAssignment(int id)
        {
            return _dbContext.Assignments.Where(a => a.AssignmentId == id).Join(_dbContext.Works, a => a.AssignmentId,
                w => w.AssignmentId, (a, w) => new {a, w}).Join(_dbContext.Employees, aw => aw.w.EmployeeId, e => e.EmployeeId, (aw, e) => e);
        }

        public Task GetTaskOnAssignment(int id)
        {
            return _dbContext.Assignments.Where(a => a.AssignmentId == id).Join(_dbContext.Tasks, a => a.TaskId, t => t.TaskId, (a, t) => t).FirstOrDefault();
        }

        public void CreateAssignment(Assignment assignment)
        {
            if (assignment == null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }

            _dbContext.Assignments.Add(assignment);
        }

        public void UpdateAssignment(Assignment assignment)
        {
            //Nothing
        }

        public void DeleteAssignment(Assignment assignment)
        {
            if (assignment == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Assignments.Remove(assignment);
        }
    }
}