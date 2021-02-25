using System.Collections.Generic;
using System.Linq;
using AssignmentService.Dto;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public interface IEmployeeRepository
    {
        bool SaveChanges();
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);

        Employee GetLoginCredentials(EmployeeDto login);
        IQueryable<Assignment> GetAssignmentsOfEmployee(int id);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}