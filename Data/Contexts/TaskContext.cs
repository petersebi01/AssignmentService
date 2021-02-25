﻿﻿using AssignmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentService.Data.Contexts
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> contextOptions) : base(contextOptions) {}
        public DbSet<Task> Tasks { get; set; }
    }
}