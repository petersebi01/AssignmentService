using AssignmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentService.Data.Contexts
{
    public class AssignmentServiceDbContext : DbContext
    {
        public AssignmentServiceDbContext(DbContextOptions<AssignmentServiceDbContext> contextOptions) : base(contextOptions) {}
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Work> Works { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}