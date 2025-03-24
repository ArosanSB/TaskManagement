using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Serilog;

namespace Application.UseCases.Tasks;

public class GetAllTasks : IUseCase<IEnumerable<TaskItemDto>>
{
    private readonly ITaskReposistory _taskRepository;
    private readonly IMapper _mapper;
    public GetAllTasks(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TaskItemDto>> Execute()
    {
        try
        {
            IEnumerable<TaskItemEntity> taskItems = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskItemDto>>(taskItems).ToList();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error getting all task task");

            throw new Exception($"Failed to get all task", ex);
        }
    }
}
