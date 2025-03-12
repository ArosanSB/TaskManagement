using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITaskManagementReposistory
    {
        Task<IEnumerable<TaskItemEntity>> GetAllAsync();
        Task<TaskItemEntity> GetByIdAsync(Guid id);
        Task AddAsync(TaskItemEntity task);
        Task UpdateAsync(TaskItemEntity task);
        Task DeleteAsync(Guid id);
    }
}
