using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITaskReposistory
    {
        Task<IEnumerable<TaskItemEntity>> GetAllAsync();
        Task<TaskItemEntity> GetTaskByIdAsync(Guid id);
        Task<TaskItemEntity> AddAsync(TaskItemEntity task);
        Task<TaskItemEntity> UpdateAsync(TaskItemEntity task);
        Task<bool> DeleteAsync(Guid id);
    }
}
