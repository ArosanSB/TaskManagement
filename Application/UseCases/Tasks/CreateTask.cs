using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Guards;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tasks;

public record CreateTaskRequest(string title, string description, DateTime dueDate, bool isCompleted);
public class CreateTask : IUseCase<CreateTaskRequest, ResponseDto>
{
    private readonly ITaskReposistory _taskRepository;
    private readonly IMapper _mapper;

    public CreateTask(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Execute(CreateTaskRequest request)
    {
        Guard.ThrowIfStringIsNullOrEmpty(request.title, nameof(request.title));
        Guard.ThrowIfArgumentNull(request.dueDate, nameof(request.dueDate));
        Guard.ThrowIfArgumentNull(request.isCompleted, nameof(request.isCompleted));
        Guard.ThrowIfStringIsNullOrEmpty(request.description, nameof(request.description));

        TaskItemDto taskItem = new TaskItemDto
        {
            Id = Guid.NewGuid(),
            Title = request.title,
            Description = request.description,
            DueDate = request.dueDate,
            IsCompleted = request.isCompleted
        };
        try
        {
            await _taskRepository.AddAsync(_mapper.Map<TaskItemEntity>(taskItem));
            return new ResponseDto { IsSuccess = true, Message = "Task: " + taskItem.Title + " has been created!" };
        }
        catch
        {
            return new ResponseDto { IsSuccess = false, Message = "Task: " + taskItem.Title + " has not been created!" };
        }
    }
}
