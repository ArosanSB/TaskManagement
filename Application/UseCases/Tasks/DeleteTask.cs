using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Tasks;

public record DeleteTaskRequest(Guid TaskId);
public class DeleteTask : IUseCase<DeleteTaskRequest, ResponseDto>
{
    private readonly ITaskReposistory _taskRepository;
    public DeleteTask(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
    }

    public async Task<ResponseDto> Execute(DeleteTaskRequest request)
    {
        try
        {
            await _taskRepository.DeleteAsync(request.TaskId);
            return new ResponseDto { IsSuccess = true, Message = "Task has been deleted!" };
        }
        catch
        {
            return new ResponseDto { IsSuccess = false, Message = "Task has not been deleted!" };
        }
    }
}
