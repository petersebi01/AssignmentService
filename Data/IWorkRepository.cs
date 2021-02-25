using System.Collections.Generic;
using AssignmentService.Models;

namespace AssignmentService.Data
{
    public interface IWorkRepository
    {
        bool SaveChanges();
        IEnumerable<Work> GetAllWorks();
        
        Work GetWorkById(int id);
        
        void CreateWork(Work work);
        
        void UpdateWork(Work work);
        
        void DeleteWork(Work work);
    }
}