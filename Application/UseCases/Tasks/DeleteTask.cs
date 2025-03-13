using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Guards;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Serilog;

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
        Guard.ThrowIfArgumentNull(request.TaskId, nameof(request.TaskId));
        try
        {
            await _taskRepository.DeleteAsync(request.TaskId);
            return new ResponseDto
            {
                IsSuccess = true,
                Message = $"Task: {request.TaskId} has been deleted!"
            };
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error delete task:", request.TaskId);

            throw new Exception($"Failed to delete task: {request.TaskId}", ex);
        }
    }
}
