using System;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

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
        try
        {
            TaskItemEntity taskItemEntity = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            return _mapper.Map<TaskItemDto>(taskItemEntity);
        }
        catch
        {
            //TODO log error + throw expection
            return null;
        }
    }
}
