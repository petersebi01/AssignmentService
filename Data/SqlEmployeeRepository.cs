using System;
using System.Collections.Generic;
using System.Linq;
using AssignmentService.Data.Contexts;
using AssignmentService.Dto;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AssignmentServiceDbContext _dbContext;

        public SqlEmployeeRepository(AssignmentServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(p => p.EmployeeId == id);
        }

        public Employee GetLoginCredentials(EmployeeDto login)
        {
            return _dbContext.Employees.SingleOrDefault(e =>
                e.Username == login.Username && e.Password == login.Password);
        }

        public IQueryable<Assignment> GetAssignmentsOfEmployee(int id)
        {
            return _dbContext.Employees.Where(e => e.EmployeeId == id).Join(_dbContext.Works, e => e.EmployeeId, w => w.EmployeeId, (e, w) =>
                w ).Join(_dbContext.Assignments, w => w.AssignmentId, a => a.AssignmentId, (w, a) => a);
        }

        public void CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _dbContext.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            //
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Employees.Remove(employee);
        }
    }
}