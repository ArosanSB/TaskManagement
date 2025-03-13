using System.Threading.Tasks;
using Application.Dto;
using Application.Guards;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

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

        try
        {
            await _taskRepository.UpdateAsync(_mapper.Map<TaskItemEntity>(request.TaskItem));
            return new ResponseDto { IsSuccess = true, Message = "Task: " + request.TaskItem.Title + " has been created!" };
        }
        catch
        {
            //TODO log error + throw expection
            return new ResponseDto { IsSuccess = false, Message = "Task: " + request.TaskItem.Title + " has not been created!" };

        }
    }
}
