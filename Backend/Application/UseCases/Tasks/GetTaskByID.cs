using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Guards;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Serilog;

namespace Application.UseCases.Tasks;

public record GetTaskByIDRequest(Guid TaskId);
public class GetTaskByID : IUseCase<GetTaskByIDRequest, TaskItemDto>
{
    private readonly ITaskReposistory _taskRepository;
    private readonly IMapper _mapper;
    public GetTaskByID(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
        _mapper = mapper;
    }

    public async Task<TaskItemDto> Execute(GetTaskByIDRequest request)
    {
        Guard.ThrowIfArgumentNull(request.TaskId, nameof(request.TaskId));
        try
        {
            TaskItemEntity taskItemEntity = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            return _mapper.Map<TaskItemDto>(taskItemEntity);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error getting task by id task: {TaskTitle}", request.TaskId);

            throw new Exception($"Failed to update task: {request.TaskId}", ex);
        }
    }
}
