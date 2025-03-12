using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Persistance.Repositories
{
    internal class TaskManagementReposistory : ITaskManagementReposistory
    {
        public TaskManagementReposistory(BusinessLogicDbContext dbContext) : base(dbContext)
        {
        }

        public Task AddAsync(TaskItemEntity task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItemEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskItemEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TaskItemEntity task)
        {
            throw new NotImplementedException();
        }
    }
}
