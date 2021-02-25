using System;
using System.Collections.Generic;
using System.Linq;
using AssignmentService.Data.Contexts;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public class SqlWorkRepository : IWorkRepository
    {
        private readonly AssignmentServiceDbContext _dbContext;
        
        public SqlWorkRepository(AssignmentServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public IEnumerable<Work> GetAllWorks()
        {
            return _dbContext.Works.ToList();
        }

        public Work GetWorkById(int id)
        {
            return _dbContext.Works.FirstOrDefault(p => p.WorkId == id);
        }

        public void CreateWork(Work work)
        {
            if (work == null)
            {
                throw new ArgumentNullException(nameof(work));
            }

            _dbContext.Works.Add(work);
        }

        public void UpdateWork(Work work)
        {
            //
        }

        public void DeleteWork(Work work)
        {
            if (work == null)
            {
                throw new ArgumentNullException();
            }

            _dbContext.Works.Remove(work);
        }
    }
}