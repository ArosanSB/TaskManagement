using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repositories;

public class TaskReposistory : ITaskReposistory
{
    private readonly BusinessLogicDbContext _context;

    public TaskReposistory(BusinessLogicDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<TaskItemEntity> AddAsync(TaskItemEntity task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        TaskItemEntity taskItemEntity = await _context.Tasks.FindAsync(id);
        _context.Tasks.Remove(taskItemEntity);
    }

    public async Task<IEnumerable<TaskItemEntity>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItemEntity> GetTaskByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<TaskItemEntity> UpdateAsync(TaskItemEntity task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task SetIsCompleted(Guid id, bool isCompleted)
    {
        TaskItemEntity task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            task.IsCompleted = isCompleted;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

    }
}
