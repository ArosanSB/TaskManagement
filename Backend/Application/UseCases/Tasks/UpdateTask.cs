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

public record UpdateTaskRequest(TaskItemDto TaskItem);
public class UpdateTask : IUseCase<UpdateTaskRequest, ResponseDto>
{
    private readonly ITaskReposistory _taskRepository;
    private readonly IMapper _mapper;
    public UpdateTask(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
        _mapper = mapper;
    }
    public async Task<ResponseDto> Execute(UpdateTaskRequest request)
    {
        Guard.ThrowIfArgumentNull(request.TaskItem, nameof(request.TaskItem));
        Guard.ThrowIfArgumentNull(request.TaskItem.DueDate, nameof(request.TaskItem.DueDate));
        Guard.ThrowIfArgumentNull(request.TaskItem.IsCompleted, nameof(request.TaskItem.IsCompleted));
        Guard.ThrowIfStringIsNullOrEmpty(request.TaskItem.Description, nameof(request.TaskItem.Description));
        Guard.ThrowIfStringIsNullOrEmpty(request.TaskItem.Title, nameof(request.TaskItem.Title));
        try
        {
            var taskEntity = _mapper.Map<TaskItemEntity>(request.TaskItem);
            await _taskRepository.UpdateAsync(taskEntity);

            return new ResponseDto
            {
                IsSuccess = true,
                Message = $"Task: {request.TaskItem.Title} has been updated!"
            };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating task: {TaskTitle}", request.TaskItem.Title);

            throw new Exception($"Failed to update task: {request.TaskItem.Title}", ex);
        }
    }
}
